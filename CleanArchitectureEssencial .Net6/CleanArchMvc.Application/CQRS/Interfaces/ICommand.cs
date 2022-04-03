namespace CleanArchMvc.Application.CQRS.Interfaces
{
    public interface ICommand
    {
        bool Validate();
    }
}