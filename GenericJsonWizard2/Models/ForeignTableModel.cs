﻿using GenericJsonWizard.BackingData;

namespace GenericJsonWizard.Models;

internal class ForeignTableModel : TableModel
{
    private ForeignTableData _Data { get; set; }

    public ForeignTableModel(ForeignTableData data) : base(data)
    {
        _Data = data;
    }

    protected override void CreateTempTableClause()
    {
        if (_Data.HasId && _Data.HasBackfillId)
        {
            Scr($"CREATE TEMP TABLE {_Data.BackfillId.Alias()} AS");
            Scr("(", 1);
        }
    }

    protected override void ReturningClause()
    {
        if (_Data.HasId && _Data.HasBackfillId)
        {
            Scr("RETURNING *");
            Scr(")", -1);
            Scr("SELECT * FROM _inserted");
        }
    }

    protected override void FinalClause()
    {
        if (_Data.HasId && _Data.HasBackfillId)
        {
            Scr(")", -1);
        }
    }
}
