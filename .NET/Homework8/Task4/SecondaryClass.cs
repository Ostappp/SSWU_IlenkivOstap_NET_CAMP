namespace Homework8.Task4
{
    internal class SecondaryClass : BaseClass
    {
        string name;
        public SecondaryClass(string name)
        {
            this.name = name;
            //можна підписуватись та відписуватись
            Event += Execution1;
            
        }
        //якщо зробити подію віртуальною, то появляється змога її викликати
        //та взаємодіяти (взаємодія не обмежена підпискою/відпискою)
        protected override event Task4Delegate Event;

        public override string? ToString()
        {
            return base.ToString();
        }

        /// <summary>
        /// не можна викликати у лдочірніх класах івенти
        /// </summary>
        //public void InvokeDelegate()
        //{
        //    Delegate();
        //}
        string Execution1()
        {
            return $"{GetType().Name}\tname: {name}\ttime: {DateTime.Now}";
        }
    }
}
