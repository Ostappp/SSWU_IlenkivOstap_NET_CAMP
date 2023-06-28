namespace Homework5.Task2.Interfaces
{
    internal interface IManager
    {
        public IManagable ManagedObj { get; }
        public string ManageObject(IManagable obj, string commandToExecute, out string report);
        public string Manual { get; }
        
    }
}
