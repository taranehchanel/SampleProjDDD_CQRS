﻿namespace Api.Utilities
{
	/// <summary>
	/// 
	/// </summary>
	[Microsoft.AspNetCore.Mvc.ApiController]

	[Microsoft.AspNetCore.Mvc.Route
		(template: Constants.Routing.Controller)]
	public class ControllerBase : Microsoft.AspNetCore.Mvc.ControllerBase
	{

        #region Constructor
        public ControllerBase() : base()
        {
        }
        #endregion /Constructor
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="unitOfWork"></param>
        //public ControllerBase(Persistence.IUnitOfWork unitOfWork)
        //{
        //	UnitOfWork = unitOfWork;
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        //protected Persistence.IUnitOfWork UnitOfWork { get; }
    }
}
