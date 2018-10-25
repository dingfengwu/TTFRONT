/**
 * Created by Newton on 13-11-23.
 */
$(function (){
  var nav = '';
  var banner = $('#banner');
  var swipe = new Swipe(banner[0], {
    startSlide: 0,
    speed: 400,
    auto: 3000,
    continuous: true,
    disableScroll: false,
    stopPropagation: false,
    callback: function (index){
      banner.find('.ui-banner-nav li')
        .removeClass('active')
        .eq(index)
        .addClass('active');
    }
  });

  banner.find('.ui-banner-wrap').children().each(function (i){
    nav += '<li' + (swipe.getPos() === i ? ' class="active"' : '') + '></li>';
  });

  banner.find('.ui-banner-nav').html(nav);

  var ua = navigator.userAgent.toLowerCase();
  var isWeixin = /MicroMessenger/i.test(ua);

  if (isWeixin) {
    var weixinPopup = $('#weixin-tip');

    // 关闭弹出
    weixinPopup.on('click', '.close', function (e){
      e.preventDefault();

      weixinPopup.addClass('fn-hide');
    });

    weixinPopup.removeClass('fn-hide');
  }
});