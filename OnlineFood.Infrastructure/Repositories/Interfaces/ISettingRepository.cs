using OnlineFood.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Infrastructure.Repositories.Interfaces
{
   public interface ISettingRepository
    {
        UserSetting GetSetting();
        void Insert(UserSetting entity);
        void Update(UserSetting entity);       
    }
}
