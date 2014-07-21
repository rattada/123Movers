using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace _123Movers.Models
{
    public static class Constants
    {
        //public static enum Terms
        //{
        //    Recurring = "0",
        //    NonRecurring = "1",
        //    RecurringWithNotice = "2"

        //}
        public const string BASE_JS_FOLDER = "/Scripts/";
        public static readonly string VIEW = "~/Views/Shared/_CompanyInfo.cshtml";
        public static readonly string URL = "/Account/LogOff";

        public static readonly string NEW_BUDGET = "NEW";
        public static readonly string RENEWL_BUDGET = "RENEWAL INSERTION";
        public static readonly string EDIT_BUDGET = "EDIT";

        public static readonly int LOCAL = 1009;
        public static readonly int LONG = 1000;
        public static readonly int BOTH = 999;
        public static readonly string LOCAL_TEXT = "Local";
        public static readonly string LONG_TEXT = "Long";
        public static readonly string BOTH_TEXT = "Both";
        public static readonly string DEFAULT = "Default";

        public static readonly string Recurring = "0";
        public static readonly string NonRecurring = "1";
        public static readonly string RecurringWithNotice = "2";


        #region All  Stored ProceduresConstants

        #region Search Stored Procedures
        public static readonly string SP_COMPANY_SEARCH = "usp_CompanySearchv3";
        #endregion

        #region AreaCode Stored Procedures
        public static readonly string SP_GET_COMPANY_STATE_AREACODE_PRICE = "usp_GetCompanyStateAreacodePrice";
        public static readonly string SP_COMPANY_AREACODE_ADD = "up_companyAreacodeAdd";
        public static readonly string SP_COMPANY_AREACODE_DELETE = "up_companyAreacodeDelete";
        public static readonly string SP_ADD_COMPANY_PRICE_PERLEAD = "usp_AddCompanyPricePerLead";
        public static readonly string SP_GET_AVAILABLE_AREAS = "usp_availableAreas";
        #endregion


        #region Budget Stored Procedures
        public static readonly string SP_GET_COMPANY_BUDGET = "usp_GetCompanyBudget";
        public static readonly string SP_SAVE_BUDGET = "usp_SaveBudget";
        public static readonly string SP_GET_AREACODES_STATES = "usp_getAreaCodesStates";
        public static readonly string SP_GET_FILTER_RESULT = "usp_GetFiltersInfo";
        #endregion

        #region OriginZipCodes Stored Procedures
        public static readonly string SP_GET_COMPANY_SERVICE_AREACODES = "usp_GetCompanyServiceAreaCodes";
        public static readonly string SP_ADD_COMPANY_AREA_ORIGIN_ZIPCODES = "usp_AddCompanyAreasOriginZipCodes";
        public static readonly string SP_DELETE_COMPANY_AREA_ORIGIN_ZIPCODES = "usp_DeleteCompanyAreasOriginZipCodes";
        public static readonly string SP_GET_COMPANY_AVAILABLE_AREA_ORIGIN_ZIPCODES = "usp_GetCompanyAvailableAreasOrignZipCodes";
        public static readonly string SP_GET_COMPANY_AREA_ORIGIN_ZIPCODES = "usp_GetCompanyAreasZipCodes";

        #endregion

        #region DestinationZipCodes Stored Procedures
        public static readonly string SP_ADD_COMPANY_AREADEST_ZIPCODES = "usp_AddCompanyAreasDestZipCodes";
        public static readonly string SP_DELETE_COMPANY_AREADEST_ZIPCODES = "usp_DeleteCompanyAreasDestZipCodes";
        public static readonly string SP_GET_COMPANY_AVAILABLE_AREADEST_ZIPCODES = "usp_GetCompanyAvailableAreasDestinationZipCodes";
        public static readonly string SP_GET_COMPANY_AREADEST_ZIPCODES = "usp_GetCompanyAreasDestinationZipCodes";
        #endregion

        #region LeadLimit Stored Procedures
        public static readonly string SP_GET_COMPANY_LEADLIMIT = "usp_GetCompanyLeadLimit";
        public static readonly string SP_ADD_COMPANY_LEADLIMIT = "usp_AddCompanyLeadLimit";
        #endregion

        #region MoveDistance Stored Procedures
        public static readonly string SP_GET_COMPANY_MOVEDISTANCE = "usp_GetCompanyMoveDistance";
        public static readonly string SP_ADD_COMPANY_MOVEDISTANCE = "usp_SaveMoveDistance";
        #endregion

        #region MoveWeight Stored Procedures
        public static readonly string SP_GET_MOVESIZE_LOOKUP = "usp_GetMoveSizeLookup";
        public static readonly string SP_ADD_COMPANY_MOVEWEIGHT = "usp_SaveMoveWeight";

        #endregion

        #region Notifications Stored Procedures


        #endregion

        #region Reports Stored Procedures


        #endregion

        #endregion
    }
}