namespace Api.Utilities
{
    public class ControllerBaseWithDatabaseContext:ControllerBase
    {
        #region Constructor
        public ControllerBaseWithDatabaseContext
            (Persistence.DatabaseContext databaseContext) : base()
        {
            DatabaseContext = databaseContext;
        }
        #endregion /Constructor

        #region Properties
        protected Persistence.DatabaseContext DatabaseContext { get; init; }
        #endregion /Properties
    }
}
