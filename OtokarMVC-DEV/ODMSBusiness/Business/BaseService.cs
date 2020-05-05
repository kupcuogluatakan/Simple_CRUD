using ODMSCommon;
using ODMSCommon.Security;
using System.Collections.Generic;

namespace ODMSBusiness
{
    public abstract class BaseService<T>
    {
        public virtual ResponseModel<T> Get(UserInfo user, T filter)
        {
            var response = new ResponseModel<T>();
            response.Model = filter;
            return response;
        }

        public virtual ResponseModel<T> Select(UserInfo user, T filter)
        {
            var response = new ResponseModel<T>();
            response.Model = filter;
            return response;
        }

        public virtual ResponseModel<T> List(UserInfo user, T filter)
        {
            var response = new ResponseModel<T>();
            response.Data = new List<T>();
            return response;
        }

        public virtual ResponseModel<T> Insert(UserInfo user, T model)
        {
            var response = new ResponseModel<T>();
            response.Model = model;
            return response;
        }

        public virtual ResponseModel<T> Update(UserInfo user, T model)
        {
            var response = new ResponseModel<T>();
            response.Model = model;
            return response;
        }

        public virtual ResponseModel<T> Delete(UserInfo user, T model)
        {
            var response = new ResponseModel<T>();
            response.Model = model;
            return response;
        }

        public virtual ResponseModel<bool> Exists(UserInfo user, T filter)
        {
            var response = new ResponseModel<bool>();
            response.Model = false;
            return response;
        }
    }
}
