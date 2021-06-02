// app.js
App({
  onLaunch() {
    var data = {major:10053,minor:7342,latitude:31.338687,longitude:120.777152};
    wx.request({
      url:"http://e.mdsd.cn:9111/qiandao/do.asp",
      data:data,
      header:{
          //"Content-Type":"application/json",
          //"Content-Type":"text/html",
          'content-type': 'application/x-www-form-urlencoded',
          "Upgrade-Insecure-Requests":1,
          "Accept-Language": "zh-CN",
          "Cookie":"ASPSESSIONIDSQAQCAAD=OKKNDMIDPCLDLEBBAKKOINMB;"//可以，就是要预先得到 Cookie
      },
      success:function(res){
          var cookie = res.header["Set-Cookie"];
          if (cookie != null) {
            console.log('得到cookie:'+cookie);
            //wx.setStorageSync("sessionid", res.header["Set-Cookie"]);
          }
          else
          {
            console.log('没有得到cookie');  
          }

          console.log(res.data);
          wx.showToast({  
            title: '成功',  
            icon: 'success',  
            duration: 3000  
          });
      },
      fail:function(err){
          console.log(err)
          wx.showToast({  
            title: '失败',  
            icon: 'loading...',  
            duration: 3000  
          });
      }
  });
  },
  globalData: {
    userInfo: null
  }
})