namespace GenericJsonWizard.EtlaToolbelt.Wizards;

public interface IFormDataWithCreateMethod<TResult, TArg>
{
    public abstract static TResult Create(TArg arg);
}
