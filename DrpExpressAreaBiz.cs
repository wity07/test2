using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.MVCUtils;
using Common.Patterns;
using Fx.BaseCenter.KnownObjects;
using Site;
using Models.Model.DisorBack.DrpExpressArea;
using Common; 

namespace Biz.DisorBack
{
    public class DrpExpressAreaBiz : Singleton<DrpExpressAreaBiz>
    {
        public Tuple<List<DrpExpressArea>, long> GetList(DrpExpressAreaIndexRequest request, int? pageNo = null, int? pageSize = null)
        { 
            var apiSDKClient = ApiProxy.CreateClient();

            var field = new LambdaFields<DrpExpressArea>().
              SelectFields(x => new
              {
                  x.Areas,
                  x.DrpEAreaID,
                  x.DrpExpressID,
                  x.FirstWeight,
                  x.FirstWeightPrice,
                  x.FreePrice,
                  x.Name,
                  x.Remark,
                  x.AreaNames,
                  x.DrpExpressName,
                  x.WarehouseName,
                  x.Sort,
                  x.RenewWeight,
                  x.RenewWeightPrice,
              }).Fields;

            var result = apiSDKClient.Request<DrpExpressAreaGetListResponse>(new DrpExpressAreaGetListRequest()
            {
                Fields = field,
                DrpExpressID=request.DrpExpressID,
                DrpSiteID=request.DrpSiteID,
                TenantID=request.TenantID,
                WarehouseID=request.WarehouseID,
                PageNo=pageNo,
                PageSize=pageSize
            });
            return result.IsBizSuccess ? new Tuple<List<DrpExpressArea>, long>(result.DrpExpressAreas, result.TotalCounts)
                                     : new Tuple<List<DrpExpressArea>, long>(null, 0);
            
        }

        public DrpExpressArea GetDetail(long drpEAreaId, long tenantId)
        {
            
            var apiSDKClient = ApiProxy.CreateClient();
            var field = new LambdaFields<DrpExpressArea>().
              SelectFields(x => new
              {
                  x.Areas,
                  x.DrpEAreaID,
                  x.DrpExpressID,
                  x.FirstWeight,
                  x.FirstWeightPrice,
                  x.FreePrice,
                  x.Name,
                  x.Remark,
                  x.WarehouseID,
                  x.RenewWeight,
                  x.RenewWeightPrice,
                  x.Sort
              }).Fields;

            var result = apiSDKClient.Request<DrpExpressAreaGetResponse>(new DrpExpressAreaGetRequest()
            {
                Fields = field,
                DrpEAreaID=drpEAreaID,
                TenantID=tenantID
            });
            return result.IsBizSuccess ? result.DrpExpressArea : new DrpExpressArea();
              
        }


        public Tuple<bool, string> Create(DrpExpressAreaItem model, long drpSiteId, long executorId, List<int> areas)
        { 
            var apiSDKClient = ApiProxy.CreateClient();

            var result = apiSDKClient.Request<DrpExpressAreaCreateResponse>(new DrpExpressAreaCreateRequest()
            {
                DrpSiteID = drpSiteId,
                ExecutorID = executorId,
                FirstWeight = item.FirstWeight,
                FirstWeightPrice = item.FirstWeightPrice,
                Remark = item.Remark.TryString(),
                RenewWeight = item.RenewWeight,
                RenewWeightPrice = item.RenewWeightPrice,
                Areas = areas,
                FreePrice=item.FreePrice.TryDecimal(0),
                Name=item.Name.TryString(),
                DrpExpressID=item.DrpExpressID,
                WarehouseID=item.WarehouseID,
                Sort=item.Sort.TryInt(0),
            });
            return new Tuple<bool, string>(result.IsBizSuccess, result.BizErrorMsg);
           

        }

        public Tuple<bool, string> Update(DrpExpressAreaItem model, long drpSiteId, long executorId, List<int> areas)
        {
              
            var apiSDKClient = ApiProxy.CreateClient();
            var result = apiSDKClient.Request<DrpExpressAreaUpdateResponse>(new DrpExpressAreaUpdateRequest()
            {
                DrpSiteID = drpSiteID,
                ExecutorID = executorID,
                FirstWeight = item.FirstWeight,
                FirstWeightPrice = item.FirstWeightPrice,
                Remark = item.Remark.TryString(),
                RenewWeight = item.RenewWeight,
                RenewWeightPrice = item.RenewWeightPrice,
                Areas = areas,
                FreePrice = item.FreePrice.TryDecimal(0),
                Name = item.Name.TryString(),
                DrpExpressID = item.DrpExpressID,
                WarehouseID = item.WarehouseID,
                Sort = item.Sort.TryInt(0),
                DrpEAreaID=item.DrpEAreaID
            });
            return new Tuple<bool, string>(result.IsBizSuccess, result.BizErrorMsg);
             
        }


        public Tuple<bool, string> Delete(long drpEAreaId, long executorId)
        {
             
            var apiSDKClient = ApiProxy.CreateClient();
            var result = apiSDKClient.Request<DrpExpressAreaDeleteResponse>(new DrpExpressAreaDeleteRequest()
            {
                DrpEAreaID = drpEAreaID,
                ExecutorID = executorID
            });
            return new Tuple<bool, string>(result.IsBizSuccess, result.BizErrorMsg);
             
        }
    }
}
