using GenericJsonSuite.EtlaToolbelt.Forms;

namespace GenericJsonWizard.EtlaToolbelt.Forms;

public interface IControlMapping
{
    void Load(IWizardData data);
    void Save(IWizardData data);
}
