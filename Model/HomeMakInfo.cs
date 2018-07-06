using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{

    /// <summary>
    /// 实体类——家政公司
    /// </summary>
    [Serializable]
    public class HomeMakInfo
    {
        /// <summary>
        /// 私有成员变量
        /// </summary>
        private int _nID;
        private string _HomeCode;
        private string _HomeName;
        private string _HomePass;
        private string _cid;
        private string _tRealName;
        private string _HomeMobile;
        private string _HomeMobileYz;
        private string _HomePhone;
        private string _HomeEmail;
        private string _HomePic;
        private string _HomePro;
        private string _HomeCity;
        private string _HomeIntro;
        private string _HomeIntegral;
        private string _CreatorName;
        private string _CreatorCode;
        private int _nLogNum;
        private string _dtLastTime;
        private string _dtAddTime;
        private string _tLastIP;
        private string _oCheck;
        private string _oHide;
        private string tPic;
        private int moneyAll;

        public int MoneyAll
        {
            get { return moneyAll; }
            set { moneyAll = value; }
        }

        public string TPic
        {
            get { return tPic; }
            set { tPic = value; }
        }
        private decimal lng;

        public decimal Lng
        {
            get { return lng; }
            set { lng = value; }
        }

        private decimal lat;

        public decimal Lat
        {
            get { return lat; }
            set { lat = value; }
        }
        private string tMemo;

        public string TMemo
        {
            get { return tMemo; }
            set { tMemo = value; }
        }

        /// <summary>
        /// 属性
        /// </summary>
        public int nID
        {
            get { return this._nID; }
            set { this._nID = value; }
        }
        public string HomeCode
        {
            get { return this._HomeCode; }
            set { this._HomeCode = value; }
        }
        public string HomeName
        {
            get { return this._HomeName; }
            set { this._HomeName = value; }
        }
        public string HomePass
        {
            get { return this._HomePass; }
            set { this._HomePass = value; }
        }
        public string cid
        {
            get { return this._cid; }
            set { this._cid = value; }
        }
        public string tRealName
        {
            get { return this._tRealName; }
            set { this._tRealName = value; }
        }
        public string HomeMobile
        {
            get { return this._HomeMobile; }
            set { this._HomeMobile = value; }
        }
        public string HomeMobileYz
        {
            get { return this._HomeMobileYz; }
            set { this._HomeMobileYz = value; }
        }
        public string HomePhone
        {
            get { return this._HomePhone; }
            set { this._HomePhone = value; }
        }
        public string HomeEmail
        {
            get { return this._HomeEmail; }
            set { this._HomeEmail = value; }
        }
        public string HomePic
        {
            get { return this._HomePic; }
            set { this._HomePic = value; }
        }
        public string HomePro
        {
            get { return this._HomePro; }
            set { this._HomePro = value; }
        }
        public string HomeCity
        {
            get { return this._HomeCity; }
            set { this._HomeCity = value; }
        }
        public string HomeIntro
        {
            get { return this._HomeIntro; }
            set { this._HomeIntro = value; }
        }
        public string HomeIntegral
        {
            get { return this._HomeIntegral; }
            set { this._HomeIntegral = value; }
        }
        public string CreatorName
        {
            get { return this._CreatorName; }
            set { this._CreatorName = value; }
        }
        public string CreatorCode
        {
            get { return this._CreatorCode; }
            set { this._CreatorCode = value; }
        }
        public int nLogNum
        {
            get { return this._nLogNum; }
            set { this._nLogNum = value; }
        }
        public string dtLastTime
        {
            get { return this._dtLastTime; }
            set { this._dtLastTime = value; }
        }
        public string dtAddTime
        {
            get { return this._dtAddTime; }
            set { this._dtAddTime = value; }
        }
        public string tLastIP
        {
            get { return this._tLastIP; }
            set { this._tLastIP = value; }
        }
        public string oCheck
        {
            get { return this._oCheck; }
            set { this._oCheck = value; }
        }
        public string oHide
        {
            get { return this._oHide; }
            set { this._oHide = value; }
        }
    }
}
