﻿using GenericJsonSuite;
using GenericJsonWizard.BackingData;
using GenericJsonWizard.BackingData.ColumnMetadata;
using GenericJsonWizard.Models;
using System.Text;
using System.Text.Json;

namespace GenericJsonWizard;

internal static class Boilerplate
{
    internal static string GenerateTargetSql()
    {
        StringBuilder builder = new();
        int indent = 0;

        var jsonColumns = ChosenData.GetAllVisibleJsonColumns(showJsonEntities: true);
        foreach (var col in jsonColumns)
        {
            if (col.JsonType == JsonValueKind.Object || col.JsonType == JsonValueKind.Array)
            {
                var model = new TypeModel(col);
                builder.AppendLine(model.Defn(indent));
            }
        }

        foreach (DomainTableData domain in ChosenData.DomainTables)
        {
            var model = new DomainTableModel(domain);
            builder.Append(model.Defn(true));
        }

        foreach (ForeignTableData foreign in ChosenData.ForeignTables)
        {
            var model = new ForeignTableModel(foreign);
            builder.Append(model.Defn(true));
        }

        foreach (TargetTableData other in ChosenData.TargetTables)
        {
            var model = new TargetTableModel(other);
            builder.Append(model.Defn(true));
        }
        return builder.ToString();
    }

    internal static string GenerateStagingSql()
    {
        StringBuilder builder = new();
        int indent = 0;

        builder.AppendLine("----------------------------------------------------------------------------------");
        builder.AppendLine($"--\tThis SQL script was generated by GenericFeedWizard.  Created: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
        builder.AppendLine($"--\tAny changes you make will be lost if you re-run GenericFeedWizard.");
        builder.AppendLine("----------------------------------------------------------------------------------");

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Process function
        //

        //  Preamble and definition
        builder.AppendLine();
        builder.AppendLine($"CREATE OR REPLACE PROCEDURE {ChosenData.QualifiedProcessProc}(_load_id INTEGER, _args VARCHAR)");
        builder.AppendLine("LANGUAGE plpgsql");
        builder.AppendLine("SECURITY DEFINER");
        builder.AppendLine("AS $procedure$");
        builder.AppendLine("DECLARE");
        builder.AppendLine($"\t_feed_name varchar := '{ChosenData.FeedDetails.FeedFullName}';");
        builder.AppendLine("\t_jsonArgs jsonb:= cast(_args as jsonb);");
        builder.AppendLine("\t_bad_count int;");
        builder.AppendLine("BEGIN");
        ++indent;

        // Domains
        foreach (var domain in ChosenData.DomainTables)
        {
            DomainTableModel model = new(domain);
            builder.AppendLine(model.CheckValues(indent));
        }

        // Foreign Tables
        foreach (var foreign in ChosenData.ForeignTables)
        {
            ForeignTableModel model = new(foreign);
            builder.Append(model.Populate(indent));
        }

        //// MultiMapped Tables
        //foreach (var multi in ChosenData.MultiMappedTables)
        //{
        //    MultiMapTableModel model = new(multi);
        //    builder.Append(model.Populate(indent));
        //}

        // TargetTables
        foreach (var other in ChosenData.TargetTables)
        {
            if (!other.IsStructureOnly)
            {
                TargetTableModel model = new(other);
                builder.AppendLine(model.Populate(indent));
            }
        }

        //if (!ChosenData.HasDataSourcesTable && ChosenData.DataSourceDetails.FinishedAt)
        //{
        //    builder.AppendLine($"--Note the finish time (it won't be identical to that in {ChosenData.StagingSchema}._loads)");
        //    builder.AppendLine("--");
        //    builder.AppendLine($"\t{ChosenData.QualifiedDataSourcesTable} SET finished_at = clock_timestamp();");
        //    builder.AppendLine();
        //}

        builder.AppendLine("END;");
        builder.AppendLine("$procedure$;");
        builder.AppendLine();
        builder.AppendLine($"ALTER PROCEDURE {ChosenData.QualifiedProcessProc}(INT, VARCHAR) OWNER TO {ChosenData.Roles.StagingOwner};");
        builder.AppendLine($"GRANT execute ON PROCEDURE {ChosenData.QualifiedProcessProc}(INT, VARCHAR) to {ChosenData.Roles.DbLoader};");
        builder.AppendLine();
        builder.AppendLine();

        builder.AppendLine(GenerateFeedDefinitions());

        return builder.ToString();

    }

    internal static string GenerateFeedDefinitions()
    {
        StringBuilder builder = new();
        builder.AppendLine("--========================================================================================================================");
        builder.AppendLine("-- Populate the feed definition schema tables");
        builder.AppendLine("--");
        builder.AppendLine();
        builder.AppendLine();

        var definitionShema = ChosenData.FeedDefinitionSchema;
        var stagingSchema = ChosenData.StagingSchema;
        var feedFullName = ChosenData.FeedDetails.FeedFullName;
        var populateProcedure = ChosenData.QualifiedProcessProc;
        var feedDir = Path.GetDirectoryName(ChosenData.FeedDetails.Filepath);
        var feedFile = Path.GetFileName(ChosenData.FeedDetails.Filepath);

        builder.AppendLine("--------------------------------------------------------------------------------"); ;
        builder.AppendLine("-- The _feeds table");
        builder.AppendLine("--");
        builder.AppendLine($"DELETE FROM {definitionShema}._feeds WHERE feed_name='{feedFullName}';");
        builder.AppendLine($"\t\tINSERT INTO {definitionShema}._feeds(feed_name,staging_schema_name,staging_table_name,process_proc_name,field_delimiter,reader_variety,parser_variety,feed_dir,feed_filename,structure_info)");
        builder.AppendLine($"\t\tVALUES('{feedFullName}','{stagingSchema}',NULL,'{populateProcedure}',NULL,'JSON', 'JSON,'{feedDir}','{feedFile}',NULL);");
        builder.AppendLine();
        builder.AppendLine();

        builder.AppendLine("--------------------------------------------------------------------------------"); ;
        builder.AppendLine("-- The _json_map table mapping original json names to their SQL equivalents");
        builder.AppendLine("--");
        builder.AppendLine($"DELETE FROM {definitionShema}._json_map WHERE feed_full_name = '{feedFullName}';");
        builder.AppendLine($"INSERT INTO {definitionShema}._json_map(feed_full_name,json_name, sql_name) VALUES");
        bool first = true;
        foreach (JsonColumn jsonCol in ChosenData.JsonColumns)
        {
            if (first) { first = false; } else { builder.AppendLine(","); }
            builder.Append($"('{feedFullName}','{jsonCol.JsonName}','{jsonCol.SqlName}')");
        }
        builder.AppendLine(";");
        builder.AppendLine();
        builder.AppendLine();

        builder.AppendLine("--------------------------------------------------------------------------------"); ;
        builder.AppendLine("-- The _definitions table where the user choices are held for safekeeping");
        builder.AppendLine("--");
        builder.AppendLine($"DELETE FROM {definitionShema}._definitions WHERE feed_name = '{feedFullName}';");
        builder.AppendLine($"INSERT INTO {definitionShema}._definitions(feed_name,definition) VALUES");
        builder.AppendLine($"'{ChosenData.ToJson()}';");
        builder.AppendLine();
        builder.AppendLine();

        // If we have a custom pre-transfer function defined, add in its definition
        if (ChosenData.ScriptData.HasCustomPreTransfer && ChosenData.ScriptData.PreTransferScript.IsBlack())
        {
            builder.AppendLine(ChosenData.ScriptData.PreTransferScript);
        }

        // If we have a custom post-transfer function defined, add in its definition
        if (ChosenData.ScriptData.HasCustomPostTransfer && ChosenData.ScriptData.PostTransferScript.IsBlack())
        {
            builder.AppendLine(ChosenData.ScriptData.PostTransferScript);
        }

        // If there is any additional SQL defined, add it in here
        if (ChosenData.ScriptData.AdditionalStagingScript.IsBlack())
        {
            builder.AppendLine(ChosenData.ScriptData.AdditionalStagingScript);
        }

        return builder.ToString();
    }

    internal static string GenerateTargetSchema()
    {
        StringBuilder builder = new();

        string schema = ChosenData.TargetSchema;

        RolesData roles = ChosenData.Roles;
        string schema_owner = roles.TargetSchemaUsers;
        string schema_specific_user = roles.TargetSchemaUsers;
        string db_power_user = roles.DbPowerUsers;
        string staging_owner = "staging_owner";

        builder.AppendLine("--========================================================================================================================");
        builder.AppendLine("-- Ensure all the relevant roles exist");
        builder.AppendLine();
        builder.AppendLine("-- The schema owner");
        builder.AppendLine("--");
        builder.AppendLine(CreateRoleIfNotExists(schema_owner));
        builder.AppendLine();
        if (ChosenData.FeedDetails.DbName == "epic")
        {
            builder.AppendLine("-- The read-only schema-specific group (which can select from all the tables in this schema)");
            builder.AppendLine("--");
            builder.AppendLine(CreateRoleIfNotExists(schema_specific_user));
            builder.AppendLine();
        }
        builder.AppendLine("-- The read-only database-specific group (which can select from all the data tables in all the schemas in this database");
        builder.AppendLine("--");
        builder.AppendLine(CreateRoleIfNotExists(db_power_user));
        builder.AppendLine();
        builder.AppendLine("-- The staging owner (used for loading data, which needs privileges on all the tables in this schema except delete");
        builder.AppendLine("--");
        builder.AppendLine(CreateRoleIfNotExists(staging_owner));
        builder.AppendLine();

        builder.AppendLine("--========================================================================================================================");
        builder.AppendLine("-- Create the schema");
        builder.AppendLine("--");
        builder.AppendLine($"CREATE SCHEMA IF NOT EXISTS {schema} AUTHORIZATION {schema_owner};");
        builder.AppendLine();
        builder.AppendLine();

        builder.AppendLine("--========================================================================================================================");
        builder.AppendLine("-- Privileges needed for transferring data from staging to target");
        builder.AppendLine("--");
        builder.AppendLine($"GRANT usage ON SCHEMA {schema} TO staging_owner;");
        builder.AppendLine();
        builder.AppendLine();

        if (ChosenData.FeedDetails.DbName == "epic")
        {
            builder.AppendLine("--========================================================================================================================");
            builder.AppendLine("-- User privileges");
            builder.AppendLine("--");
            builder.AppendLine($"GRANT usage ON SCHEMA {schema} TO {schema_specific_user};");
            builder.AppendLine();
            builder.AppendLine();
        }

        return builder.ToString();
    }

    internal static string CreateRoleIfNotExists(string role, bool canLogin = false, string? isMemberOf = null)
    {
        StringBuilder builder = new();
        builder.AppendLine($"-- DROP ROLE {role};");
        builder.AppendLine("DO $$");
        builder.AppendLine("\tBEGIN");
        builder.AppendLine($"\t\tIF EXISTS(select 1 from  pg_catalog.pg_roles where rolname = '{role}')");
        builder.AppendLine("\t\tTHEN");
        builder.AppendLine($"\t\t\tRAISE NOTICE 'Role {role} already exists. Skipping.';");
        builder.AppendLine("\t\tELSE");
        builder.Append($"\t\t\tCREATE ROLE {role} WITH NOSUPERUSER NOCREATEDB NOCREATEROLE INHERIT {(canLogin ? "" : "NO")}LOGIN NOREPLICATION NOBYPASSRLS ");
        builder.AppendLine($"{(isMemberOf != null ? "\n\t\t\t\tIN ROLE " + isMemberOf : "")};");
        builder.AppendLine("\t\tEND IF;");
        builder.AppendLine("\tEND $$;");
        return builder.ToString();
    }

    public static string GenerateDatabase()
    {
        StringBuilder builder = new();

        var db = ChosenData.FeedDetails.DbName;

        RolesData roles = ChosenData.Roles;
        string dbOwner = roles.DbOwner;

        builder.AppendLine("-- Create the (no-login) database owner if it doesn't exist");
        builder.AppendLine("--");
        builder.AppendLine(CreateRoleIfNotExists(dbOwner));
        builder.AppendLine();
        builder.AppendLine();

        builder.AppendLine("-- Create the database with the appropriate ownership");
        builder.AppendLine("-- N.B. This cannot be done from within a transaction");
        builder.AppendLine("--");
        builder.AppendLine($"CREATE DATABASE {db} OWNER {dbOwner};");
        builder.AppendLine();
        builder.AppendLine();



        return builder.ToString();
    }

    internal static string GenerateLoadingInfrastructure()
    {
        StringBuilder builder = new();

        RolesData roles = ChosenData.Roles;
        string stagingOwner = roles.StagingOwner;
        string loaderGroupName = roles.LoaderGroupName;
        string dbLoader = roles.DbLoader;
        string dbPowerusers = roles.DbPowerUsers;

        var stagingSchema = ChosenData.StagingSchema;
        var feedDefinitionSchema = ChosenData.FeedDefinitionSchema;

        builder.AppendLine("--========================================================================================================================");
        builder.AppendLine("-- Create the generic staging_owner role used by the GenericFeedLoader if it doesn't exist");
        builder.AppendLine("--");
        builder.AppendLine(CreateRoleIfNotExists(stagingOwner));
        builder.AppendLine();
        builder.AppendLine();
        builder.AppendLine("-- Create the generic loaders group with used by the GenericFeedLoader if it doesn't exist");
        builder.AppendLine("--");
        builder.AppendLine(CreateRoleIfNotExists(loaderGroupName, canLogin: true));
        builder.AppendLine();
        builder.AppendLine();
        builder.AppendLine("-- Create the db_specific loader role, with login, used by the GenericFeedLoader if it doesn't exist");
        builder.AppendLine("--");
        builder.AppendLine(CreateRoleIfNotExists(dbLoader, canLogin: true, isMemberOf: loaderGroupName));
        builder.AppendLine();
        builder.AppendLine();

        builder.AppendLine("--========================================================================================================================");
        builder.AppendLine("-- Create the feed definitions schema");
        builder.AppendLine("--");
        builder.AppendLine($"CREATE SCHEMA IF NOT EXISTS {feedDefinitionSchema} AUTHORIZATION {stagingOwner};");
        builder.AppendLine($"GRANT USAGE ON SCHEMA {feedDefinitionSchema} TO {loaderGroupName}");
        builder.AppendLine($"GRANT USAGE ON SCHEMA {feedDefinitionSchema} TO {dbPowerusers}");
        builder.AppendLine();
        builder.AppendLine();
        builder.AppendLine($"--DROP TABLE {feedDefinitionSchema}._definitions;");
        builder.AppendLine();
        builder.AppendLine($"CREATE TABLE {feedDefinitionSchema}._definitions(");
        builder.AppendLine("\tfeed_name text NOT NULL,");
        builder.AppendLine("\tdefinition text NULL,");
        builder.AppendLine("\tPRIMARY KEY(feed_name)");
        builder.AppendLine(");");
        builder.AppendLine();
        builder.AppendLine($"ALTER TABLE {feedDefinitionSchema}._definitions OWNER TO {stagingOwner};");
        builder.AppendLine($"GRANT SELECT ON {feedDefinitionSchema}._definitions TO {dbPowerusers};");
        builder.AppendLine();
        builder.AppendLine();
        builder.AppendLine($"--DROP TABLE {feedDefinitionSchema}._feeds;");
        builder.AppendLine();
        builder.AppendLine($"CREATE TABLE {feedDefinitionSchema}._feeds(");
        builder.AppendLine("\tfeed_id int NOT NULL GENERATED ALWAYS AS IDENTITY,");
        builder.AppendLine("\tfeed_name text NOT NULL,");
        builder.AppendLine("\tfeed_dir text NULL,");
        builder.AppendLine("\tfeed_filename text NULL,");
        builder.AppendLine("\tparser_variety text NULL DEFAULT 'CSV',");
        builder.AppendLine("\treader_variety text NULL DEFAULT 'TABULAR',");
        builder.AppendLine("\tstructure_info text NULL,");
        builder.AppendLine("\tfield_delimiter text NOT NULL DEFAULT ',',");
        builder.AppendLine("\ttrim_white_space bool NOT NULL DEFAULT false,");
        builder.AppendLine("\tcomment_token text NULL,");
        builder.AppendLine("\tquoted_fields bool NULL DEFAULT true,");
        builder.AppendLine($"\tstaging_schema_name NOT NULL text DEFAULT '{feedDefinitionSchema}',");
        builder.AppendLine("\tstaging_table_name text NOT NULL,");
        builder.AppendLine("\tinit_proc_name text NULL,");
        builder.AppendLine("\tprocess_proc_name text NULL,");
        builder.AppendLine("\tPRIMARY KEY(feed_name)");
        builder.AppendLine(");");
        builder.AppendLine();
        builder.AppendLine($"ALTER TABLE {feedDefinitionSchema}._feeds OWNER TO {stagingOwner};");
        builder.AppendLine($"GRANT SELECT ON {feedDefinitionSchema}._feeds TO {dbPowerusers};");
        builder.AppendLine();
        builder.AppendLine();
        builder.AppendLine($"--DROP TABLE {feedDefinitionSchema}._groups;");
        builder.AppendLine();
        builder.AppendLine($"CREATE TABLE {feedDefinitionSchema}._groups(");
        builder.AppendLine("\tgroup_name text NOT NULL,");
        builder.AppendLine($"\tstaging_schema text NOT NULL DEFAULT '{ChosenData.StagingSchema}',");
        builder.AppendLine("\tdefinition jsonb NOT NULL,");
        builder.AppendLine("\tPRIMARY KEY(group_name)");
        builder.AppendLine(");");
        builder.AppendLine();
        builder.AppendLine($"ALTER TABLE {feedDefinitionSchema}._groups OWNER TO {stagingOwner};");
        builder.AppendLine($"GRANT SELECT ON {feedDefinitionSchema}._groups TO {dbPowerusers};");
        builder.AppendLine();
        builder.AppendLine();
        builder.AppendLine($"--DROP TABLE {feedDefinitionSchema}._metadata;");
        builder.AppendLine();
        builder.AppendLine($"CREATE TABLE {feedDefinitionSchema}._metadata(");
        builder.AppendLine("\ttable_schema varchar(255) NOT NULL,");
        builder.AppendLine("\ttable_name varchar(255) NOT NULL,");
        builder.AppendLine("\tcolumn_name varchar(255) NOT NULL,");
        builder.AppendLine("\tfield_name text NULL,");
        builder.AppendLine("\tto_null text NULL,");
        builder.AppendLine("\tfrom_null text NULL,");
        builder.AppendLine("\tPRIMARY KEY(table_schema, table_name, column_name)");
        builder.AppendLine(");");
        builder.AppendLine();
        builder.AppendLine($"ALTER TABLE {feedDefinitionSchema}._metadata OWNER TO {stagingOwner};");
        builder.AppendLine($"GRANT SELECT ON {feedDefinitionSchema}._metadata TO {dbPowerusers};");
        builder.AppendLine();
        builder.AppendLine();
        builder.AppendLine($"--DROP TABLE {feedDefinitionSchema}._value_maps;");
        builder.AppendLine();
        builder.AppendLine($"CREATE TABLE {feedDefinitionSchema}._value_maps(");
        builder.AppendLine("\tfeed_name text NOT NULL,");
        builder.AppendLine("\tcolumn_name varchar(59) NOT NULL,");
        builder.AppendLine("\tfeed_value text NOT NULL,");
        builder.AppendLine("\ttable_value text NOT NULL,");
        builder.AppendLine("\tPRIMARY KEY(feed_name, column_name, feed_value)");
        builder.AppendLine(");");
        builder.AppendLine();
        builder.AppendLine($"ALTER TABLE {feedDefinitionSchema}._value_maps OWNER TO {stagingOwner};");
        builder.AppendLine($"GRANT SELECT ON {feedDefinitionSchema}._value_maps TO {dbPowerusers};");
        builder.AppendLine();
        builder.AppendLine();
        builder.AppendLine($"CREATE OR REPLACE VIEW {feedDefinitionSchema}._column_metadata");
        builder.AppendLine("AS SELECT c.table_schema,");
        builder.AppendLine("\tc.table_name,");
        builder.AppendLine("\tc.column_name,");
        builder.AppendLine("\tc.ordinal_position,");
        builder.AppendLine("\tc.data_type,");
        builder.AppendLine("\tc.character_maximum_length,");
        builder.AppendLine("\tc.is_nullable,");
        builder.AppendLine("\tCOALESCE(m.field_name, c.column_name::text) AS field_name,");
        builder.AppendLine("\tm.to_null,");
        builder.AppendLine("\tm.from_null");
        builder.AppendLine("FROM information_schema.columns c");
        builder.AppendLine($"LEFT JOIN {feedDefinitionSchema}._metadata m ON c.table_schema::name = m.table_schema::text AND c.table_name::name = m.table_name::text AND c.column_name::name = m.column_name::text");
        builder.AppendLine("WHERE(c.table_schema::name<> ALL(ARRAY['information_schema'::name, 'pg_catalog'::name])) AND(c.column_name::name<> ALL(ARRAY['is_error'::name, 'error_description'::name]))");
        builder.AppendLine("ORDER BY c.table_name, c.ordinal_position;");
        builder.AppendLine();
        builder.AppendLine($"ALTER TABLE {feedDefinitionSchema}._column_metadata OWNER TO {stagingOwner};");
        builder.AppendLine($"GRANT SELECT ON {feedDefinitionSchema}._column_metadata TO {dbPowerusers};");
        builder.AppendLine();
        builder.AppendLine();
        builder.AppendLine($"CREATE OR REPLACE VIEW {feedDefinitionSchema}._feeds_view");
        builder.AppendLine("AS SELECT _feeds.feed_id,");
        builder.AppendLine("\t_feeds.feed_name,");
        builder.AppendLine("\t_feeds.feed_dir,");
        builder.AppendLine("\t_feeds.feed_filename,");
        builder.AppendLine("\t_feeds.parser_variety,");
        builder.AppendLine("\t_feeds.reader_variety,");
        builder.AppendLine("\t_feeds.structure_info,");
        builder.AppendLine("\t_feeds.field_delimiter,");
        builder.AppendLine("\t_feeds.trim_white_space,");
        builder.AppendLine("\t_feeds.comment_token,");
        builder.AppendLine("\t_feeds.quoted_fields,");
        builder.AppendLine("\t_feeds.staging_schema_name,");
        builder.AppendLine("\t_feeds.staging_table_name,");
        builder.AppendLine("\t_feeds.init_proc_name,");
        builder.AppendLine("\t_feeds.process_proc_name,");
        builder.AppendLine("\t_feeds.feed_dir AS feed_directory_format,");
        builder.AppendLine("\t_feeds.feed_filename AS feed_filename_format,");
        builder.AppendLine("\t_feeds.init_proc_name AS initialisation_procedure_name,");
        builder.AppendLine("\t_feeds.process_proc_name AS finalisation_procedure_name,");
        builder.AppendLine("\t_feeds.reader_variety AS header_variety,");
        builder.AppendLine("\tNULL::text AS logging_directory_format,");
        builder.AppendLine("\tNULL::text AS log_filename_format,");
        builder.AppendLine("\tNULL::text AS bad_line_filename_format");
        builder.AppendLine($"FROM {feedDefinitionSchema}._feeds;");
        builder.AppendLine();
        builder.AppendLine($"ALTER VIEW {feedDefinitionSchema}._feeds_view OWNER TO {stagingOwner};");
        builder.AppendLine($"GRANT SELECT ON {feedDefinitionSchema}._feeds_view TO {dbPowerusers};");
        builder.AppendLine();
        builder.AppendLine();
        //ToDo: All the 


        builder.AppendLine("--========================================================================================================================");
        builder.AppendLine("-- Create the staging schema");
        builder.AppendLine("--");
        builder.AppendLine($"CREATE SCHEMA IF NOT EXISTS {stagingSchema} AUTHORIZATION {stagingOwner};");
        builder.AppendLine();
        builder.AppendLine($"GRANT USAGE ON {stagingSchema} TO {loaderGroupName};");
        builder.AppendLine();
        builder.AppendLine();
        builder.AppendLine($"--DROP TABLE {stagingSchema}._loads;");
        builder.AppendLine();
        builder.AppendLine($"CREATE TABLE {stagingSchema}._loads(");
        builder.AppendLine("\tid int NOT NULL GENERATED ALWAYS AS IDENTITY,");
        builder.AppendLine("\trun_id int NOT NULL,");
        builder.AppendLine("\tfeed_id int NULL,");
        builder.AppendLine("\tfilepath text NULL,");
        builder.AppendLine("\tfile_created timestamptz NULL,");
        builder.AppendLine("\tfile_modified timestamptz NULL,");
        builder.AppendLine("\tstarted_at timestamptz NOT NULL DEFAULT clock_timestamp(),");
        builder.AppendLine("\tfinished_at timestamptz NULL,");
        builder.AppendLine("\tdata_source_id int NULL,");
        builder.AppendLine("\tlines_in_file bigint NULL,");
        builder.AppendLine("\tignored_lines bigint NULL,");
        builder.AppendLine("\tbad_records bigint NULL,");
        builder.AppendLine("\tgood_records bigint NULL,");
        builder.AppendLine("\tbad_rows bigint NULL,");
        builder.AppendLine("\tignored_filtered_rows bigint NULL,");
        builder.AppendLine("\tignored_conflict_rows bigint NULL,");
        builder.AppendLine("\tignored_duplicate_rows bigint NULL,");
        builder.AppendLine("\tmerged_out_rows bigint NULL,");
        builder.AppendLine("\tgood_rows bigint NULL,");
        builder.AppendLine("\tupdated_rows bigint NULL,");
        builder.AppendLine("\tinserted_rows bigint NULL,");
        builder.AppendLine("\tmessage text NULL,");
        builder.AppendLine("\tPRIMARY KEY(id)");
        builder.AppendLine(");");
        builder.AppendLine();
        builder.AppendLine($"ALTER TABLE {stagingSchema}._loads OWNER TO {stagingOwner};");
        builder.AppendLine($"GRANT SELECT ON {stagingSchema}._loads TO {dbPowerusers};");
        builder.AppendLine();
        builder.AppendLine();
        builder.AppendLine($"--DROP TABLE {stagingSchema}._problems;");
        builder.AppendLine();
        builder.AppendLine($"CREATE TABLE {stagingSchema}._problems(");
        builder.AppendLine("\tload_id int NOT NULL,");
        builder.AppendLine("\tline_number int NOT NULL,");
        builder.AppendLine("\terror_description text NULL,");
        builder.AppendLine("\tPRIMARY KEY(load_id, line_number)");
        builder.AppendLine(");");
        builder.AppendLine();
        builder.AppendLine($"ALTER TABLE {stagingSchema}._problems OWNER TO {stagingOwner};");
        builder.AppendLine($"GRANT SELECT ON {stagingSchema}._problems TO {dbPowerusers};");
        builder.AppendLine();
        builder.AppendLine();
        builder.AppendLine($"--DROP TABLE {stagingSchema}._runs;");
        builder.AppendLine();
        builder.AppendLine($"CREATE TABLE {stagingSchema}._runs(");
        builder.AppendLine("\tid int NOT NULL GENERATED ALWAYS AS IDENTITY,");
        builder.AppendLine("\tcmd_line text NULL,");
        builder.AppendLine("\tstarted_at timestamptz DEFAULT clock_timestamp() NOT NULL,");
        builder.AppendLine("\tfinished_at timestamptz NULL,");
        builder.AppendLine("\tmessage text NULL,");
        builder.AppendLine("\tPRIMARY KEY(id)");
        builder.AppendLine(");");
        builder.AppendLine();
        builder.AppendLine($"ALTER TABLE {stagingSchema}._runs OWNER TO {stagingOwner};");
        builder.AppendLine($"GRANT SELECT ON {stagingSchema}._runs TO {dbPowerusers};");
        builder.AppendLine();
        builder.AppendLine();
        builder.AppendLine($"--DROP TABLE {stagingSchema}._timings;");
        builder.AppendLine();
        builder.AppendLine($"CREATE TABLE {stagingSchema}._timings(");
        builder.AppendLine("\tload_id int NOT NULL,");
        builder.AppendLine("\tstamp timestamptz DEFAULT clock_timestamp() NOT NULL,");
        builder.AppendLine("\tmsg text NOT NULL,");
        builder.AppendLine("\tPRIMARY KEY(load_id, stamp, msg)");
        builder.AppendLine(");");
        builder.AppendLine();
        builder.AppendLine($"ALTER TABLE {stagingSchema}._timings OWNER TO {stagingOwner};");
        builder.AppendLine($"GRANT SELECT ON {stagingSchema}._timings TO {dbPowerusers};");
        builder.AppendLine();
        builder.AppendLine();

    var getLoadId = $"""
        -- DROP FUNCTION {stagingSchema}._get_load_id(int, text);

        CREATE OR REPLACE FUNCTION {stagingSchema}._get_load_id(_run_id integer, _args text)
            RETURNS integer
            LANGUAGE plpgsql
            SECURITY DEFINER
        AS $function$
        DECLARE
            _jsonArgs jsonb := cast(_args as jsonb);
            _feed_id integer := _jsonArgs -> 'feed_id';
            _filepath text := _jsonArgs ->> 'filepath';
            _file_created timestamp := _jsonArgs ->> 'file_created';
            _file_modified timestamp := _jsonArgs ->> 'file_modified';
            _load_id integer;
        BEGIN
            INSERT INTO  {stagingSchema}._loads(run_id, feed_id, filepath, file_created, file_modified)
            VALUES (_run_id, _feed_id, _filepath, _file_created, _file_modified) returning id into _load_id;

            RETURN _load_id;
        END;
        $function$
        ;

        -- Permissions

        ALTER FUNCTION {stagingSchema}._get_load_id(int, text) OWNER TO {stagingOwner};
        GRANT EXECUTE ON FUNCTION {stagingSchema}._get_load_id(int, text) TO {dbPowerusers};
""";
        return builder.ToString();
    }
}