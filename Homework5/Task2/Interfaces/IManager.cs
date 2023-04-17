namespace Homework5.Task2.Interfaces
{
    internal interface IManager
    {
        public string ManageObject(IManagable obj, string commandToExecute, out string report);
        
    }
}
