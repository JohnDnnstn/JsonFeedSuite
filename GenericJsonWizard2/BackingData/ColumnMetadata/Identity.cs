﻿using GenericJsonSuite;

namespace GenericJsonWizard.BackingData.ColumnMetadata;

public class Identity : Metadata
{
    public string SerialName { get; set; } = "";

    public override string Default {
        get
        {
            if (SerialName.IsWhite())
            {
                return $" GENERATED BY DEFAULT AS IDENTITY";
            }
            else
            {
                return $" DEFAULT nextval('{SerialName}')";
            }
        }
        set { }
    }

    /// <summary>Constructor used by Persistence ONLY</summary>
    public Identity() : base() => Init();

    /// <summary>The normal constructor</summary>
    /// <param name="sqlName">The name of the identity column in the database</param>
    public Identity(string sqlName) : base(sqlName) => Init();

    private void Init()
    {
        IsId = true;
        Variety = MetadataVariety.ID;
        SqlType = "INT";
        Nullable = false;
    }

    public override string ToString()
    {
        return SqlName;
    }
}
