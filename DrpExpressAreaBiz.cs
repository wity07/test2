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
using FXB_Client = Fx.BaseCenter.Provider.Client;

namespace Biz.DisorBack
{
    public class DrpExpressAreaBiz : Singleton<DrpExpressAreaBiz>
    {
        public Tuple<List<DrpExpressArea>, long> GetList(DrpExpressAreaIndexRequest request, int? pageNo = null, int? pageSize = null)
        {
             #region 注销老Api调用
            /*
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
             */
             #endregion

            #region 新Api调用
            var result = FXB_Client.ThriftProxy.DrpExpressArea_GetList(request.TenantID.HasValue?request.TenantID.Value:0,request.DrpSiteID.HasValue?request.DrpSiteID.Value:0,request.DrpExpressID.HasValue?request.DrpExpressID.Value:0,request.WarehouseID.HasValue?request.WarehouseID.Value:0, pageNo.HasValue ? pageNo.Value : 1, pageSize.HasValue ? pageSize.Value : 15).Result;
            return result.Count > 0
                ? new Tuple<List<DrpExpressArea>, long>(result.Data.Select(info => new DrpExpressArea
                {
                    DrpSiteID = info.DrpSiteID,
                    DrpExpressID = info.DrpExpressID,
                    FirstWeight = info.FirstWeight,
                    FirstWeightPrice = info.FirstWeightPrice,
                    Name = info.Name,
                    AreaNames = info.AreaNames,
                    Areas = info.Areas,
                    DrpEAreaID = info.DrpEAreaID,
                    DrpExpressName = info.DrpExpressName,
                    WarehouseName = info.WarehouseName,
                    WarehouseID = info.WarehouseID,
                   Remark = info.Remark,
                   RenewWeight = info.RenewWeight,
                   RenewWeightPrice = info.RenewWeightPrice
                    
                    
                }).ToList(), (long)result.Count)
             : new Tuple<List<DrpExpressArea>, long>(new List<DrpExpressArea>(), 0);
            #endregion
        }

        public DrpExpressArea GetDetail(long drpEAreaId, long tenantId)
        {
              #region 注销老Api调用
            /*
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
             */
              #endregion

            #region 新Api调用
            var result = FXB_Client.ThriftProxy.DrpExpressArea_Get(drpEAreaId, tenantId).Result;
            if (result.Result.Success)
            {
                var info = result.Data;
                string areaNames = "";
                if (info.Areas.Count() > 0)
                    areaNames = string.Join(",", info.Areas.Select(c => c.AreaName));
                info.AreaNames = areaNames;
                var data = new DrpExpressArea()
                {
                   
                   DrpSiteID = info.DrpSiteID,
                   DrpExpressID = info.DrpExpressID,
                   FirstWeight = info.FirstWeight,
                   FirstWeightPrice = info.FirstWeightPrice,
                   Name = info.Name,
                   AreaNames = info.AreaNames,
                   Areas = info.Areas,
                   DrpEAreaID = info.DrpEAreaID,
                   DrpExpressName = info.DrpExpressName,
                   WarehouseName = info.WarehouseName,
                   WarehouseID = info.WarehouseID,
                   Remark = info.Remark,
                   RenewWeight = info.RenewWeight,
                   RenewWeightPrice = info.RenewWeightPrice,
                   Sort = info.Sort,
                   FreePrice = info.FreePrice
                };
                return data;
            }
            return new  DrpExpressArea();
            #endregion
        }


        public Tuple<bool, string> Create(DrpExpressAreaItem model, long drpSiteId, long executorId, List<int> areas)
        {
              #region 注销老Api调用
            /*
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
             */
              #endregion
            #region 新Api调用
            var result = FXB_Client.ThriftProxy.DrpExpressArea_Create(new DrpExpressArea()
            {
                
                DrpSiteID = drpSiteId,
                DrpExpressID = model.DrpExpressID.HasValue?model.DrpExpressID.Value:0,
                FirstWeight = model.FirstWeight.HasValue?(double)model.FirstWeight.Value:0,
                FirstWeightPrice = model.FirstWeightPrice.HasValue ? (double)model.FirstWeightPrice : 0,
                Name = model.Name,
                DrpEAreaID = model.DrpEAreaID.HasValue?model.DrpEAreaID.Value:0,
                WarehouseID = model.WarehouseID.HasValue?model.WarehouseID.Value:0,
                Remark = model.Remark,
                RenewWeight = model.RenewWeight.HasValue?(double)model.RenewWeight.Value:0,
                RenewWeightPrice = model.RenewWeightPrice.HasValue ? (double)model.RenewWeightPrice.Value : 0,
                Areas = areas.Count>0?areas.Select(c=>new DrpExpressAreaMap()
                    {
                        AreaID = c.TryInt()
                    }).ToList():new List<DrpExpressAreaMap>()
            }, executorId).Result;

            return new Tuple<bool, string>(result.Success, result.Msg);

            #endregion

           
            

        }

        public Tuple<bool, string> Update(DrpExpressAreaItem model, long drpSiteId, long executorId, List<int> areas)
        {
              #region 注销老Api调用
            /*
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
             */
              #endregion
            #region 新Api调用
            var result = FXB_Client.ThriftProxy.DrpExpressArea_Update(new DrpExpressArea()
            {

                DrpSiteID = drpSiteId,
                DrpExpressID = model.DrpExpressID.HasValue ? model.DrpExpressID.Value : 0,
                FirstWeight = model.FirstWeight.HasValue ? (double)model.FirstWeight.Value : 0,
                FirstWeightPrice = model.FirstWeightPrice.HasValue ? (double)model.FirstWeightPrice : 0,
                Name = model.Name,
                DrpEAreaID = model.DrpEAreaID.HasValue ? model.DrpEAreaID.Value : 0,
                WarehouseID = model.WarehouseID.HasValue ? model.WarehouseID.Value : 0,
                Remark = model.Remark,
                RenewWeight = model.RenewWeight.HasValue ? (double)model.RenewWeight.Value : 0,
                RenewWeightPrice = model.RenewWeightPrice.HasValue ? (double)model.RenewWeightPrice.Value : 0,
                Areas = areas.Count > 0 ? areas.Select(c => new DrpExpressAreaMap()
                {
                    AreaID = c.TryInt()

                }).ToList() : new List<DrpExpressAreaMap>()


            }, executorId).Result;

            return new Tuple<bool, string>(result.Success, result.Msg);

            #endregion
        }


        public Tuple<bool, string> Delete(long drpEAreaId, long executorId)
        {
              #region 注销老Api调用
            /*
            var apiSDKClient = ApiProxy.CreateClient();
            var result = apiSDKClient.Request<DrpExpressAreaDeleteResponse>(new DrpExpressAreaDeleteRequest()
            {
                DrpEAreaID = drpEAreaID,
                ExecutorID = executorID
            });
            return new Tuple<bool, string>(result.IsBizSuccess, result.BizErrorMsg);
             */

              #endregion
            #region 新Api调用
            var result = FXB_Client.ThriftProxy.DrpExpressArea_Delete(drpEAreaId, executorId).Result;
            return new Tuple<bool, string>(result.Success, result.Msg);
            #endregion
        }
    }
}
