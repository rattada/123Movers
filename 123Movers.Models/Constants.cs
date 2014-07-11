using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace _123Movers.Models
{
    public static class Constants
    {
        public static readonly string URL = "/Account/LogOff";
        public static readonly string RENEWL_BUDGET = "RENEWAL INSERTION";
        public static readonly int LOCAL = 1009;
        public static readonly int LONG = 1000;
        public static readonly string VIEW = "~/Views/Shared/_CompanyInfo.cshtml";


        #region All  Stored ProceduresConstants

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
        public static readonly string SP_GET_FILTER_RESULT = "usp_FilterResult";
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
        public static readonly string SP_GET_COMPANY_AVAILABLE_AREADEST_ZIPCODES = "usp_GetCompanyAvailableAreasDestincationZipCodes";//NEED TO CHECK SPELLING
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