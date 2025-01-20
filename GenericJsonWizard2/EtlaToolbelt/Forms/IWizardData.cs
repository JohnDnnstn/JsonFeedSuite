//------------------------------------------------------------------------------------------
// This file was generated from the EtlaTool.Wizards vsn:1.0 template
// Created 29/08/2023 17:44:59
// Copyright: Etla Services Ltd 2019-2023
//------------------------------------------------------------------------------------------

namespace GenericJsonSuite.EtlaToolbelt.Forms
    ;

/// <summary>All objects that user chosen data to forms should implement this interface</summary>
public interface IWizardData
{
}

public interface IWizardData2D : IWizardData
{
    public int RowCount { get;}
    public int ColCount { get; }
    public object this[int row, int col] { get; set; }

    public void ClearData();
}
