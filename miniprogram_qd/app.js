// app.js
App({
  onLaunch() {
    console.log(123);
    wx.request({
      url:"http://e.mdsd.cn:9111/qiandao/index.asp",
      //data:data,
      header:{
         // "Content-Type":"application/json"
      },
      success:function(res){
          console.log(res.data)
      },
      fail:function(err){
          console.log(err)
      }
  })
  },
  globalData: {
    userInfo: null
  }
})
