/*!
* FitText.js 1.2
*
* Copyright 2011, Dave Rupert http://daverupert.com
* Released under the WTFPL license
* http://sam.zoy.org/wtfpl/
*
* Date: Thu May 05 14:23:00 2011 -0600
*/

(function( $ ){

  $.fn.fitText = function( kompressor, options ) {

    // Setup options
    var compressor = kompressor || 1,
        settings = $.extend({
          'minFontSize' : Number.NEGATIVE_INFINITY,
          'maxFontSize' : Number.POSITIVE_INFINITY
        }, options);

    return this.each(function(){

      // Store the object
      var $this = $(this);

      // Resizer() resizes items based on the object width divided by the compressor * 10
      var resizer = function () {
        $this.css('font-size', Math.max(Math.min($this.width() / (compressor*10), parseFloat(settings.maxFontSize)), parseFloat(settings.minFontSize)));
      };

      // Call once to set.
      resizer();

      // Call on resize. Opera debounces their resize by default.
      $(window).on('resize.fittext orientationchange.fittext', resizer);

    });

  };

})( jQuery );

var EG = {
    viewType: 'title',
    tabSlider1: null,
    tabSlider2: null,
    headerHeight: $('.header-top').outerHeight(),
    gridContainer: null,
    /* mark page as home page or subpage */
    pageMark: function(){
        var subNav = $('.header .main-nav .sub-nav');
        if(subNav.length == 0){
            $('.page-wrap').addClass('homepage');
        }else{
            $('.page-wrap').addClass('subpage');
        }
    },
    /* detect touch device */
    isTouchDevice: function(){
        var deviceAgent = navigator.userAgent.toLowerCase();
        return Modernizr.touch ||
            (deviceAgent.match(/(iphone|ipod|ipad)/) ||
                deviceAgent.match(/(android)/) ||
                deviceAgent.match(/(iemobile)/) ||
                deviceAgent.match(/iphone/i) ||
                deviceAgent.match(/ipad/i) ||
                deviceAgent.match(/ipod/i) ||
                deviceAgent.match(/blackberry/i) ||
                deviceAgent.match(/bada/i));
    },
    /* sticky header */
    stickyHeader: function(){        
		var top=0;
        $(window).scroll(function(e){
            top = $(this).scrollTop();
            EG.headerHeight = $('.header-top').outerHeight();

            if($('.page-wrap').hasClass('expanded')){
                e.preventDefault();
                e.stopPropagation();
                return;
            }
            if(top > EG.headerHeight){
                $('.main-nav, .mobile-navbar').addClass('fixed');  
                $('body').addClass('nav-fixed');           
                $('.main').addClass('top-padding');
            }else{
                $('.main-nav, .mobile-navbar').removeClass('fixed');
                $('body').removeClass('nav-fixed');
                $('.main').removeClass('top-padding');
            }

            if(top>EG.headerHeight){
                $('.back-top').stop().fadeIn(300);
            }else{
                $('.back-top').stop().fadeOut(300);
            }
        });
    },
    /* tablet nav toggle */
    navToggle: function(){
        $('.main-nav .nav-toggle').click(function(){
            $(this).toggleClass('open');
            $('.main-nav .nav').slideToggle();
            $('.main').toggleClass('navopen');
            $('.sub-nav').slideToggle();
            return false;
        });
        if(EG.isTouchDevice()){
            $(".sub-nav ul li .sub-link").click(function(){
                var flyout = $(this).parent().find('.flyout');
                var otherFlyouts = $(this).parent().siblings('li').find('.flyout');
                if(flyout.length > 0){
                    $(this).parent().siblings('li').removeClass('active');
                    $(this).parent().toggleClass('active');
                    otherFlyouts.fadeOut();
                    flyout.stop().slideToggle();
                    return false;
                }
            });
            $(".sub-nav ul li").click(function(e){
                var flyout = $(this).find('.flyout');
                if(flyout.length > 0){
                    e.stopPropagation();
                }
            });
            $(document).on('click', function () {
                $('.sub-nav .flyout').fadeOut();
                $('.sub-nav ul li').removeClass('active');
            });
        }else{
            $(".sub-nav ul li .sub-link").click(function(){
                var flyout = $(this).parent().find('.flyout');
                if(flyout.length > 0){
                    return false;
                }
            });
            $(".sub-nav ul li").each(function(){
                var timer1 = null; timer2 = null;
                var _this = $(this);
                _this.hover(
                    function(){
                        var flyout = _this.find('.flyout');
                        if(flyout.length == 0){ return;}
                        if(timer1 == null){
                            timer2 = setTimeout(function(){
                                _this.addClass('active');
                                flyout.stop().slideDown();
                                timer2 = null;
                            },500);
                        }else {
                            clearTimeout(timer1);
                            timer1 =  null;
                        }
                    },
                    function(){
                        var flyout = _this.find('.flyout');
                        if(flyout.length == 0){ return;}
                        if(timer2){
                            clearTimeout(timer2);
                            timer2 = null;
                        }else{
                            timer1 = setTimeout(function(){
                                _this.removeClass('active');
                                flyout.stop().fadeOut();
                                timer1 = null;
                            },500);
                        }
                    }
                );
            });
        }
    },

    /* preload image */
    preloadImg: function(url){
        if (document.images) {
            var img = new Image();
            img.src = url;
        }
    },
    /* mouseover image change hover image */
    imgHover: function(){
        $('img[data-hover-img]').parent().each(function(){
            var src = $(this).find('img').attr('src'),
                hoverScr = $(this).find('img').data('hover-img');
            EG.preloadImg(hoverScr);
            $(this).hover(
                function(){
                    $(this).find('img').attr('src',$(this).find('img').data('hover-img'));
                },
                function(){
                    $(this).find('img').attr('src',src);
                }
            );
        });
    },

    /* partner search link */
    handlePartnerSearch: function () {
        var tipVal = $('#partner-select').find('option').eq(0).attr('value');
        if($('#partner-select').val() == tipVal){
            $('.module-partner-search .link-search').addClass('disabled');
        }
        $('#partner-select').change(function () {
            if($(this).val() == tipVal){
                $('.module-partner-search .link-search').addClass('disabled');
            }else{
                $('.module-partner-search .link-search').removeClass('disabled');
            }
        });
        $('.module-partner-search').on('click', '.link-search.disabled', function () {
            return false;
        });
    },

    /* go to top */
    gototop:function(){
        $('.back-top').click(function () {
            $('html, body').animate({scrollTop: 0}, 1000, 'easeOutCubic');
        });
    },
    IEBrowserLower: function(DEFAULT_VERSION){
        var ua = navigator.userAgent.toLowerCase();
        var isIE = ua.indexOf("msie")>-1;
        var safariVersion;

        if(isIE){
            safariVersion =  ua.match(/msie ([\d.]+)/)[1];
            if( parseInt(safariVersion) <= parseInt(DEFAULT_VERSION) ){
                return true;
            }else{
                return false;
            }
        }else{
            return false;
        }
    },
    /* mobile side menu toggle */
    mobileNavHandle: function(){
        if($('.nav-overlay').length == 0){
            $('.page-wrap').append('<div class="nav-overlay"></div>');
        }
        //$('#menu').multilevelpushmenu({
        //    collapsed: true,
        //    fullCollapse: true,
        //    mode:'cover',
        //    menuWidth: 228,
        //    preventItemClick: false,
        //    preventGroupItemClick: true,
        //    menuHeight: '100%',
        //    containersToPush: [$('.page-wrap'), $('.mobile-navbar')],
        //    backText: 'Zurück',
        //    swipe: 'desktop', // or 'touchscreen', or 'both'
        //    onExpandMenuStart: function(){
        //        var screenHeight = window.innerHeight || $(window).height();
        //        $('.mobile-nav').css({'height': screenHeight, 'min-height': screenHeight});  
        //    },
        //    onExpandMenuEnd: function(){
        //        var screenHeight = window.innerHeight || $(window).height();
        //        $('.mobile-nav').css({'height': screenHeight, 'min-height': screenHeight});              
        //    },
        //    onItemClick: function() {
        //        var $item = arguments[2];
        //        var itemHref = $item.find( 'a:first' ).attr( 'href' );
        //        location.href = itemHref;
        //    }
        //});
        var $navtitle = $('#menu nav h2').text();
        $('.mobile-navbar .nav-toggle').click(function(){
            var $nav = $('.mobile-nav').multilevelpushmenu('findmenusbytitle' , $navtitle).first();
            var menuexpanded = $('.mobile-nav').multilevelpushmenu('menuexpanded', $nav);
            if(menuexpanded){
                $('#menu').multilevelpushmenu('collapse');
                isExpanded = false;
                $('.nav-overlay').hide();
                $(this).removeClass('icon-close');
                setTimeout(function(){$('.page-wrap').removeClass('expanded');}, 500);
            }else{
                setTimeout(function(){
                    $('#menu').multilevelpushmenu('expand');
                }, 200);
                isExpanded = true;
                $('.nav-overlay').fadeIn(300);
                $(this).addClass('icon-close');
                $('.page-wrap').addClass('expanded');               
            }
            return false;
        });
        $(document).on('click', '.nav-overlay', function(){
            $('.mobile-nav').multilevelpushmenu('collapse');
            isExpanded = false;
            $('.nav-overlay').hide();
            $('.mobile-navbar .nav-toggle').removeClass('icon-close');
            setTimeout(function(){$('.page-wrap').removeClass('expanded');}, 500);
        });
        $(document).on('touchmove', '.nav-overlay', function(e){
            e.preventDefault();
            e.stopPropagation();
        });

        //$(window).resize(function () {
        //    $('.mobile-nav').multilevelpushmenu('redraw');
        //});

        //$(window).on("orientationchange", function () {
        //    $('.mobile-nav').multilevelpushmenu('redraw');
        //});
    },
    /* mobile footer menu toggle */
    footerMenuToggle: function(){
        if($(window).width() < 767){
            $(document).off('click', '.footer-links .bottom-row .item h5 a');
            $(document).on('click', '.footer-links .bottom-row .item h5 a', function(){
                $(this).parent().toggleClass('open');
                $(this).parent().parent().find('ul').slideToggle();
                return false;
            });
        }else{
            $(document).off('click', '.footer-links .bottom-row .item h5 a');
        }
    },
    /* module image slider */
    imageSlider: function(){
        $('.image-slider').each(function(){
            var _this = $(this),
                isAuto = false,
                zoomIcon = _this.parents('.module-slider').find('.view');
            if(_this.parents('.module-slider').data('auto')){
                isAuto = true;
            }
            if(_this.children('li').length > 1){
            	/*if(_this.parent().hasClass('bx-viewport')){
            		return;
            	}*/
                var imgSlider = _this.bxSlider({
                    //pager: false,
                    adaptiveHeight: true,
                    auto:isAuto,
                    pause:5000, // The amount of time (in ms) between each auto transition
                    onSliderLoad: function(currentIndex){
                        var imgHeight = _this.children('li').not('.bx-clone').eq(currentIndex).find('img').height(),
                            captionHeight = _this.children('li').not('.bx-clone').eq(currentIndex).find('.caption').height();
                        _this.children('li.bx-clone').find('a').removeAttr('rel');
                        _this.parents('.bx-wrapper').find('.bx-prev').css('top', imgHeight/2);
                        _this.parents('.bx-wrapper').find('.bx-next').css('top', imgHeight/2);
                        _this.parents('.bx-wrapper').find('.bx-pager').css('bottom', captionHeight + 15);
                    },
                    onSlideAfter: function($slideElement, oldIndex, newIndex){
                        var imgHeight = $slideElement.find('img').height(),
                            captionHeight = $slideElement.find('.caption').height();
                        _this.parents('.bx-wrapper').find('.bx-prev').css('top', imgHeight/2);
                        _this.parents('.bx-wrapper').find('.bx-next').css('top', imgHeight/2);
                        _this.parents('.bx-wrapper').find('.bx-pager').css('bottom', captionHeight + 15);
                    }
                });

                if(zoomIcon.length > 0){
                    zoomIcon.click(function(){
                        var current = imgSlider.getCurrentSlide();
                        _this.find('li').not('.bx-clone').eq(current).find('a').trigger('click');
                    });
                }

                $(window).on("orientationchange", function () {
                    imgSlider.reloadSlider();
                });
                $(window).resize(function(){
                	imgSlider.reloadSlider();
                });
            }else{
                 if(zoomIcon.length > 0){
                    zoomIcon.click(function(){                    
                        _this.find('li').not('.bx-clone').eq(0).find('a').trigger('click');
                    });
                }
            }
        });
    },
    /* home page banner slider */
    bannerSlider: function(){
        var slider = $('.bxslider').bxSlider({
            useCSS: false,
            //controls: false,
            auto: true,
            pause: 5000,
            speed: 800,
            easing: 'easeInExpo',
            onSliderLoad: function(){
                $('.banner .caption .overlay').eq(0).fadeIn(800);
                //$('.banner .caption .container').append('<span class="bx-prev"></span><span class="bx-next"></span>');
                $('.banner .bx-controls-direction a').wrapAll('<div class="container"></div>');
            },
            onSlideBefore: function($slideElement, oldIndex, newIndex){
                //$('.banner .captions ul li').eq(oldIndex).fadeOut(800);
                $('.banner ul li .caption .overlay').fadeOut(800);

            },
            onSlideAfter: function($slideElement, oldIndex, newIndex){
                //$('.banner .captions ul li').eq(newIndex).fadeIn(800);
                $slideElement.find('.caption .overlay').fadeIn(800);
            }
        });
        $(window).on("orientationchange", function () {
            slider.reloadSlider();
        });
    },
    /* product slider */
    productSlider: function(){
        $('.module-product-slider ul').bxSlider({
            minSlides: 1,
            maxSlides: 4,
            slideWidth: 250,
            slideMargin: 10,
            moveSlides: 1,
            pager: false,
            infiniteLoop: false,
            hideControlOnEnd: true
        });
    },
    /* rb-interactive slider  */
    rbslider:function(){
        $('.rb-interactive-slider').each(function () {
            var isAuto= false;
            if($(this).parent('.rb-interactive-slider-wrapper').data('auto')){
                isAuto = true;
            }
            $(this).bxSlider({
                auto:isAuto,
                pause:5000
            });
        });

        $('.text-slider-wrapper #text-slider').bxSlider({
            infiniteLoop: false,
            adaptiveHeight: true,
            hideControlOnEnd: true
        });
    },
    /* rb-interactive flexslider  */
    flexslider:function(){
        $('.media-content.slides').bxSlider({
            minSlides: 1,
            maxSlides: 2,
            slideWidth: 226,
            slideMargin: 18,
            moveSlides: 2,
            infiniteLoop: false,
            hideControlOnEnd: true
        });
    },

    /* 3d image carousel */
    imgCarousel:function () {
        $('.module-image-carouse').carousel3D();
    },

    /* productDataTable responsive table */
    producttable:function () {
        $( '.module-product-data-table' ).trigger( "enhance.tablesaw" );
        $('.tablesaw .btn-more').click(function () {
        	$(this).hide();
        	$(this).siblings('.btn-detail, .btn-less').css("display", "inline-block");
            $(this).parent().siblings('.hide-col').css("display", "block");
        });
        $('.tablesaw .btn-less').click(function () {
        	$(this).hide();
        	$(this).siblings('.btn-detail').hide();
        	$(this).siblings('.btn-more').css("display", "inline-block");
        	$(this).parent().siblings('.hide-col').css("display", "none");
        });
    },

    /* newsletter layer slider */
    layerslider:function(){
        var laslidernum = $('.newsletter-slider').bxSlider({
            infiniteLoop: false,
            pager:true,
            pagerType:'short',
            hideControlOnEnd: true,
            onSliderLoad:function(){
                var slidernum = $('.newsletter-slider-wrap .bx-has-pager .bx-default-pager').html();
                $('.module-newsletter-layer .slider-num').html(slidernum);
            },
            onSlideAfter:function(){
                var slidernum = $('.newsletter-slider-wrap .bx-has-pager .bx-default-pager').html();
                $('.module-newsletter-layer .slider-num').html(slidernum);
            }
        });
    },
    /* newsletter layer close */
    newsletterclose:function(){
        $('.module-newsletter-layer .fancybox-close').click(function(){
            $(this).parent().parent('.module-newsletter-layer').hide();
        });
    },

    /*  calender show */
    calendershow:function(){
        var disabledDays = ["10-12-2015","14-12-2015","16-12-2015","17-12-2015","23-12-2015","24-12-2015","28-12-2015"];

            //$( ".calender-content" ).datepicker({
            //    inline: true,
            //    yearRange : '-20:+5',
            //    showOtherMonths:true,
            //    showOn: 'both',
            //    dateFormat: 'dd-mm-yy',
            //    beforeShowDay: function(date) {
            //        var m = date.getMonth(), d = date.getDate(), y = date.getFullYear();
            //        for (i = 0; i < disabledDays.length; i++) {
            //            if($.inArray(d + '-' + (m+1) + '-' + y,disabledDays) != -1) {
            //                return [true, 'ui-state-selected', ''];
            //            }
            //        }
            //        return [true];
            //    },
            //    onChangeMonthYear: function(year, month, inst){
            //        setTimeout(function(){
            //            var calenderwarp = $('.calender-dropdown .title');
            //            var calendertitle = $('.calender-content .ui-datepicker-title').text();
            //            calenderwarp.html(calendertitle);
            //        },1);
            //    }
            //});

    },
    /* calender drop down */
    calenderdropdown:function() {
        if($(window).width() < 767){
            var calenderwarp = $('.calender-dropdown .title');
            var calendertitle = $('.calender-content .ui-datepicker-title').text();
            calenderwarp.html(calendertitle);

            $('.calender-dropdown').click(function(e){
                $(this).siblings('.calender-content').slideToggle();
                $(this).toggleClass('open');
            });
         }
    },


    /* accordion */
    rbaccordion:function(){
        $('.legacy-content .accordion header h3 a').click(function(){
            $(this).parent().parent().addClass('active').siblings('header').removeClass('active');
            $(this).parent().parent().next('article').slideDown().siblings('article').slideUp();
            return false;
        });
        $('.faq-wrap .accordion header h3 a').click(function () {
            if ($(this).parents('header').hasClass('active')){}
            else{
                $('header').removeClass('active');
                $(this).parents('header').addClass('active');
                $('header').next('article').slideUp();
                $(this).parents('header').next('article').slideDown();
            }
            return false;
        });

        $('.legacy-content .accordion header ,.faq-wrap .accordion header').first().children().children('a').trigger('click');
    },
    /* slider with thunbnail */
    thumbSlider: function(){
        $('.thumb-slider').thumbSlider();
    },
    /* image gallery slider */
    imgGallerySlider: function(){
        $('.module-image-gallery').imgGallerySlider();
    },
    /* multi-layer slider */
    multiLayerSlider: function(){
        $('.module-multi-slider').multiLayerSlider();
    },
    /* input auto complete */
    inputAutoComplete: function(){
        var availableTags = [
            "dekorvorschlag 1",
            "dekorvorschlag 2",
            "dekorvorschlag 3"
        ];
        $( "#search-list" ).autocomplete({
            source: availableTags
        });
    },
    /* form control radio */
    radioHandle: function(){
        $('input:radio').radios();
    },
    /* form control checkbox */
    checkHandle: function(){
        $('.checkbox').checkbox();
    },
    /* less than IE10  Support placeholder */
    placeholderSupport: function(){
        var isSupport = 'placeholder' in document.createElement('input');
        if(!isSupport){
            $('[placeholder]').focus(function() {
                var input = $(this);
                if (input.val() == input.attr('placeholder')) {
                    input.val('');
                    input.removeClass('placeholder');
                }
            }).blur(function() {
                var input = $(this);
                if (input.val() == '' || input.val() == input.attr('placeholder')) {
                    input.addClass('placeholder');
                    input.val(input.attr('placeholder'));
                }
            }).blur();
        }
    },
    /* product detail table handle */
    productTableHandle: function(){
        var tabLiNum = $('.module-product-tab .nav-tabs li').length;
        if(tabLiNum < 2){
            $('.module-product-tab').addClass('one-tab');
        }
        var tabPanels = $('.toggle-wrap .content-toggle.panel:first').nextAll();
        if($(window).width() < 767){
            tabPanels.find('.panel-heading').find('a').addClass('collapsed');
            tabPanels.find('.panel-collapse').removeClass('in');
        }else{
            tabPanels.find('.panel-heading').find('a').removeClass('collapsed');
            tabPanels.find('.panel-collapse').addClass('in');
        }
        $('.module-product-tab .panel').each(function(){
            var _this = $(this),
                columns = _this.find('ul');
            var cellCount = Math.max.apply(Math, columns.map(function(){
                return $(this).find('li').length;
            }).get());
            for(var i=0; i<cellCount; i++){
                var cellGroup = columns.map(function(){
                    return $(this).find('li').eq(i);
                });
                var maxHeight = Math.max.apply(Math, cellGroup.map(function(){
                    return $(this).css('height', 'auto').height();
                }).get());
                cellGroup.each(function(){
                    $(this).height(maxHeight);
                });
            }
        });
    },
    /* event handle when panel expand */
    panelExpand:function(){
        $('.content-toggle').on('shown.bs.collapse', function (){
            var _this = $(this),
                columns = _this.find('ul');
            var cellCount = Math.max.apply(Math, columns.map(function(){
                return $(this).find('li').length;
            }).get());
            for(var i=0; i<cellCount; i++){
                var cellGroup = columns.map(function(){
                    return $(this).find('li').eq(i);
                });
                var maxHeight = Math.max.apply(Math, cellGroup.map(function(){
                    return $(this).css('height', 'auto').height();
                }).get());
                cellGroup.each(function(){
                    $(this).height(maxHeight);
                });
            }
        });
    },
    /* panel toggle */
    togglePanel: function(){
        $('.filter-box .title').click(function(){
            $(this).toggleClass('open');
            $(this).siblings('.control-item').slideToggle();
        });
    },
    /* filterHandle */
    filterHandle: function(){
        $('.lead-block .select-text').click(function(){
            $(this).toggleClass('open');
            $(this).next('ul').slideToggle();
        });

        $('.lead-block li a').click(function(){
            var target = $(this).attr('href'),
             headerHeight = $(".header").height();
            if ($(target).length > 0) {
                $.scrollTo($(target), 600, { axis: 'y', easing: '', offset: { top: -headerHeight+60}});
            }
            return false;
        });
    },
    /* lightbox */
    lightbox: function () {
        $('.view-overlay').fancybox({
            maxWidth: 870,
            padding: 0,
            fitToView: true,
            width: '99%',
            autoHeight:true,
            autoSize: true,
            openEffect: 'fade',
            closeEffect: 'fade',
            scrolling: 'no',
            helpers : {
                overlay : {
                    locked : true
                }
            }
        });
    },

    /* viewiframe */
    viewiframe:function () {
        $(".view-iframe").fancybox({
          padding: 0,
          margin: [30, 0, 0, 0],
          maxWidth  : 1180,
          fitToView : true,
          width   : '90%',
          height    : '100%',
          autoSize  : false,
          closeClick  : false,
          openEffect  : 'none',
          closeEffect : 'none',
          scrolling : 'auto',
          preload   : true,
          closeBtn: false
        });
    },

    /* lightbox for video */
    videolayer: function () {
        $('.view-video').fancybox({
            maxWidth: 934,
            padding: 0,
            fitToView: true,
            width: '99%',
            autoHeight:true,
            autoSize: true,
            //type: 'iframe',
            openEffect: 'fade',
            closeEffect: 'fade',
            scrolling: 'no',
            helpers : {
                overlay : {
                    locked : false
                }
            }
        });
    },
    /* view image on lightbox */
    imgView: function () {
        var _this = this;
        $(".img-view").fancybox({
            openEffect	: 'elastic',
            closeEffect	: 'elastic',
            nextEffect: 'fade',
            prevEffect: 'fade',
            padding : 0,
            loop : false,
            closeBtn		: true,
            helpers		: {
                title	: { type : 'inside' }
            },
            afterLoad : function(){
            	this.wrap.addClass('fancybox-img-view');	
            }
        })
    },
    /* lightbox for image gallery */
    imgGallery: function () {
        var _this = this;
        $("a[rel='view-image'], a.media.image, .img-gallery").unbind().fancybox({
            openEffect	: 'elastic',
            closeEffect	: 'elastic',
            nextEffect: 'fade',
            prevEffect: 'fade',
            padding : 0,
            loop : false,
            closeBtn		: true,
            helpers		: {
                title	: { type : 'inside' }
            },
            afterShow : function(){
                var prev = '<div class="fancybox-nav fancybox-prev-holder" href="javascript:;"><span></span></div>';
                $(prev).insertBefore('.fancybox-next');

                if(_this.IEBrowserLower('8.0')){
                    $('.fancybox-nav.fancybox-prev-holder').removeClass('fancybox-prev-holder');
                }

                if($('.fancybox-prev').length>0){
                    $('.fancybox-prev-holder').hide();
                }

                if($('.fancybox-next').length<1){

                    var next = '<div class="fancybox-nav fancybox-next-holder" href="javascript:;"><span></span></div>';
                    $(next).insertAfter('.fancybox-prev');
                    if(_this.IEBrowserLower('8.0')){
                        $('.fancybox-nav.fancybox-next-holder').removeClass('fancybox-next-holder');
                    }
                }else{
                    $('.fancybox-next-holder').hide();
                }
            }
        });
        $(".padding-fancyb").unbind().fancybox({
            openEffect	: 'elastic',
            closeEffect	: 'elastic',
            nextEffect: 'fade',
            prevEffect: 'fade',
            padding : 20,
            loop : false,
            closeBtn		: true,
            helpers		: {
                title	: { type : 'inside' }
            },
            afterShow : function(){
                var prev = '<div class="fancybox-nav fancybox-prev-holder" href="javascript:;"><span></span></div>';
                $(prev).insertBefore('.fancybox-next');

                if(_this.IEBrowserLower('8.0')){
                    $('.fancybox-nav.fancybox-prev-holder').removeClass('fancybox-prev-holder');
                }

                if($('.fancybox-prev').length>0){
                    $('.fancybox-prev-holder').hide();
                }

                if($('.fancybox-next').length<1){

                    var next = '<div class="fancybox-nav fancybox-next-holder" href="javascript:;"><span></span></div>';
                    $(next).insertAfter('.fancybox-prev');
                    if(_this.IEBrowserLower('8.0')){
                        $('.fancybox-nav.fancybox-next-holder').removeClass('fancybox-next-holder');
                    }
                }else{
                    $('.fancybox-next-holder').hide();
                }
                $(".fancybox-skin").addClass("padding-fancybox");
            }
        });
    },

    /* mobile filter select */
    filterSelect:function(){
        if($(window).width() < 992){
            $('.filter-box h3').unbind().click(function(e){

                e.stopPropagation();
                $(this).toggleClass('open');
                $(this).siblings('form, .div-form, .filter-panel').slideToggle();
                $(this).parent().next().slideToggle();
            });

            $('.filter-box form, .filter-box .div-form, .div-form, .filter-box .filter-panel').unbind().click(function(e){
                e.stopPropagation();
            });

            $('body').click(function(){
                $('.filter-box form, .filter-box .filter-panel').slideUp();
            });
        }
    },

    /* corporate content side nav */
    sideNav:function(){
        $('.devices-nav-head').click(function(e){
            e.stopPropagation();
            $(this).toggleClass('open');
            $(this).next('.sidebar').slideToggle();
        });
        if($(window).width() < 991){
            $('body').click(function(){
                $('.legacy-content .mobile-nav-head').slideUp();
            });
        }
    },
    /* video lightbox */
    videoLightbox: function () {
        $(".rb-youtube, .legacy-content .view-video, .legacy-content .youtube").fancybox({
            fitToView: false,
            autoSize: false,
            maxWidth: 800,
            maxHeight: 450,
            width		: '90%',
            height		: '90%',
            helpers : {
                media : true
            }
        });
    },
    /* filter reset */
    filterReset: function(){
        $('.filter-box .btn.reset').click(function(){
            var active = $(this).hasClass('active')? true:false;
            if(active){
                $(this).removeClass('active');
                $('.filter-box label.checkbox').removeClass('checked').find('input').prop('checked',!active);
            }
            return false;
        })
    },
    /* active button when filter selected */
    activeBtn:function(){
        $('.gray-box label.checkbox').click(function(){
            setTimeout(function(){
                if($('.filter-box .gray-box label.checkbox').hasClass('checked')){
                    $('.btn-wrap a.reset').addClass('active');
                }else{
                    $('.btn-wrap a.reset').removeClass('active');
                }
            },200);
        });
    },
    /* image set slider */
    imgSetSlider:function(){
        if($(window).width()<767){
            $('.set-image-box .slider').each(function(){
                var _this = $(this);
                _this.bxSlider({
                    adaptiveHeight: true,
                    onSliderLoad: function(currentIndex){
                        var imgHeight = _this.children('li').not('.bx-clone').eq(currentIndex).find('img').height(),
                            captionHeight = _this.children('li').not('.bx-clone').eq(currentIndex).find('.caption').height();
                        _this.parents('.bx-wrapper').find('.bx-prev').css('top', imgHeight/2);
                        _this.parents('.bx-wrapper').find('.bx-next').css('top', imgHeight/2);
                        //_this.parents('.bx-wrapper').find('.bx-pager').css('bottom', captionHeight + 15);
                    },
                    onSlideAfter: function($slideElement, oldIndex, newIndex){
                        var imgHeight = $slideElement.find('img').height(),
                            captionHeight = $slideElement.find('.caption').height();
                        _this.parents('.bx-wrapper').find('.bx-prev').css('top', imgHeight/2);
                        _this.parents('.bx-wrapper').find('.bx-next').css('top', imgHeight/2);
                        //_this.parents('.bx-wrapper').find('.bx-pager').css('bottom', captionHeight + 15);
                    }
                });
            });
        }
    },
    /* list view and title view toggle */
    viewToggle: function () {
        var teaserList = $('.tesaser-view-module .teaser-list');
        EG.tagPosition();
        $('.product-filter-tools .icon-list-view').click(function(){
            if(EG.viewType == 'list'){
                return;
            }
            $(this).addClass('active').siblings().removeClass('active');
            teaserList.fadeOut(500, function(){
                teaserList.removeClass('list-view').addClass('title-view').fadeIn(500);
                EG.viewType = 'list';
            });
        });
        $('.product-filter-tools .icon-title-view').click(function(){
            if(EG.viewType == 'title'){
                return;
            }
            $(this).addClass('active').siblings().removeClass('active');
            teaserList.fadeOut(500, function(){
                teaserList.removeClass('title-view').addClass('list-view').fadeIn(500, function(){
                    EG.tagPosition();
                });
                EG.viewType = 'title';
            });
        });
    },
    /* new tag position handle */
    tagPosition: function(){
        if($('.tesaser-view-module .teaser-list.list-view').length > 0){
            $('.tesaser-view-module .teaser-list.list-view li').each(function(){
                var textHeight = $(this).find('.text').outerHeight();
                $(this).find('.tag-new-pdt').css('bottom', textHeight);
            });
        }
    },

    /* mobile tab nav slider */
    tabNavSlider: function(){
        var navTabs1 = $('.module-download-tabs .nav-tabs,.module-profil-tab .nav-tabs');
        var navTabs2 = $('.module-product-tab .nav-tabs');
        if($(window).width() <= 767){
            var liSize = navTabs1.children('li').length;
            navTabs1.addClass('tab-slider');
            if(EG.tabSlider1 != null){
                return;
            }
            if(liSize >= 3){
                EG.tabSlider1 = navTabs1.bxSlider({
                    pager: false,
                    infiniteLoop: false,
                    hideControlOnEnd: true,
                    minSlides: 2,
                    maxSlides: 3,
                    slideWidth: 213,
                    slideMargin: 0,
                    moveSlides: 1
                });
            }
        }else{
            navTabs1.removeClass('tab-slider').removeAttr('style');
            if(EG.tabSlider1 != null){
                EG.tabSlider1.destroySlider();
                EG.tabSlider1 = null;
            }
        }

        if($(window).width() <= 480){
            var liSize = navTabs2.children('li').length;
            navTabs2.addClass('tab-slider');
            if(EG.tabSlider2 != null){
                return;
            }
            if(liSize >= 3){
                EG.tabSlider2 = navTabs2.bxSlider({
                    pager: false,
                    infiniteLoop: false,
                    hideControlOnEnd: true,
                    minSlides: 2,
                    maxSlides: 3,
                    slideWidth: 213,
                    slideMargin: 0,
                    moveSlides: 1
                });
            }
        }else{
            navTabs2.removeClass('tab-slider').removeAttr('style');
            if(EG.tabSlider2 != null){
                EG.tabSlider2.destroySlider();
                EG.tabSlider2 = null;
            }
        }
    },
    /* sticky side panel */
    stickyPanel: function(){
        var tablePanel = $('.module-product-tab .tab-pane#tabs2,.module-product-tab.nachfb-tab .col-md-9');
        if(tablePanel.is(':visible') && $('.all-status').length){
            var minTop = $('.all-status').offset().top - 97;
            var maxTop = tablePanel.offset().top + tablePanel.height() - $('.all-status').height()-157;
            $(window).scroll(function(){
                var scrollTop = $(this).scrollTop();
                if(scrollTop >= minTop && scrollTop < maxTop){
                    $('.all-status').removeClass('absolute').addClass('fixed')
                }else if(scrollTop >= maxTop){
                    $('.all-status').removeClass('fixed').addClass('absolute');
                }else{
                    $('.all-status').removeClass('fixed').removeClass('absolute');
                }
            });
        }
    },
    /* checkboxChecked show */
    checkboxChecked :function(){
        $('.checkbox-show input').change(function(){
            if(this.checked){
                $(this).parent().next('.checkbox-up').slideDown();
            }else{
                $(this).parent().next('.checkbox-up').slideUp();
            }
        });
    },
     /* error tooltips */
    errorTips:function(){
        $('.popover-btn').popover({
            trigger: 'click',
            delay: { "hide": 50},
            placement: 'auto',
            html : true,
            title  : '',
            content: function() {
                var contents = $(this).attr("data-popover-content");
                return $(contents).html();
                return false;
            }
        });
        $('.popover-btn').click(function(){
            return false;
        });
         $(document,'.popover-close').click(function(){
            $('.popover-btn').popover('hide');  
         });
    },

    tooltip: function () {
        $('[data-toggle="tooltip"]').tooltip();
    },
    
    /* tooltips show */
    tooltips :function(){
        $('a[rel="popover"]').each(function(){
            var el = $(this);
            el.popover({
                trigger: 'hover',
                delay: { "hide": 50},
                placement: 'auto',
                html : true,
                title  : '',
                content: function() {
                    var contents = $(this).attr("data-popover-content");
                    return $(contents).html();
                    return false;
                }
            }).on("shown.bs.popover", function(){
                el.data("bs.popover").tip().off("mouseleave").on("mouseleave", function(){
                    setTimeout(function(){
                        el.popover("hide");
                    }, 50);
                });
            }).on("hide.bs.popover", function(ev){
                if(el.data("bs.popover").tip().is(":hover")){
                    ev.preventDefault();
                }
            });

            $(document).on('click', '.popover-close', function(e){
                if(el.data("bs.popover").tip().is(":hover")) {
                    el.next(".popover").remove();
                }
            });


        });

    },
    
    /* verification */
    verification: function(){
        function validateStep1() {
            var valid = true;
            $('#step1 .required,.module-profil-tab form .required').each(function () {
                if ($(this).is('select') && $(this).parent().is(':visible')) {
                    if ($(this).val() == 0) {
                        valid = false;
                        $(this).addClass('error');
                        $(this).next().addClass('error');
                    } else {
                        $(this).removeClass('error');
                        $(this).next().removeClass('error');
                    }
                } else if ($(this).is(':text') && $(this).is(':visible')) {
                    if ($(this).val() == '') {
                        valid = false;
                        $(this).addClass('error');
                        $(this).next().addClass('error');
                    } else {
                        $(this).removeClass('error');
                        $(this).next().removeClass('error');
                        if ($(this).hasClass('email')) {
                            var reg = /^[\w!#$%&'*+/=?^_`{|}~-]+(?:\.[\w!#$%&'*+/=?^_`{|}~-]+)*@(?:[\w](?:[\w-]*[\w])?\.)+[\w](?:[\w-]*[\w])?$/;
                            if (!reg.test($(this).val())) {
                                valid = false;
                                $(this).addClass('error');
                            }
                        }
                    }
                } else if ($(this).is(':checkbox') && $(this).parent().is(':visible')) {
                    if (!$(this).is(':checked')) {
                        valid = false;
                        $(this).parent().next().addClass('error');
                    } else {
                        $(this).parent().next().removeClass('error');
                    }
                }
                else if ($(this).is(':password') && $(this).is(':visible'))  {
                    if ($(this).val() == 0) {
                        valid = false;
                        $(this).addClass('error');

                    }else if($('#newpwd').val() != $('#pwd').val()){
                        valid = false;
                        $('#newpwd').addClass('error');
                        $('#newpwd').next().addClass('error');
                    }else {
                        $(this).removeClass('error');
                        $('#newpwd').next().removeClass('error');
                    }
                }
            });
            return valid;
        }


        $('#step1 .btn-submit,.module-profil-tab .tab-pane .red-btn').click(function () {
            var valid = validateStep1();
            if (valid) {
                $('.module-form-grey .error-text-bar').hide();
                $(this).parent().siblings('.error-text').hide();
            } else {
                $('.module-form-grey .error-text-bar').show();
                $(this).parent().siblings('.error-text').show();
                return false;
            }
        });

        $('.form-control.focus').keyup(function() {
            var inputText = $(this).val();
            if (inputText.length >= 1) {
                $(this).parent().parent().siblings('.row').children().children('.form-control').removeAttr('disabled').addClass('required');
            }else{
                $(this).parent().parent().siblings('.row').children().children('.form-control').attr('disabled','disabled').removeClass('required').removeClass('error');
            }
        });
    },

    /* waterfall */
    //waterfall: function(){
    //    $('#gallery-container').imagesLoaded( function() {
    //        EG.gridContainer = $('#gallery-container').isotope({
    //            itemSelector: '.grid-item',
    //            masonry: {
    //                columnWidth: 1,
    //                gutter: 0
    //            }
    //        });
    //    });
    //    $('#box-container').imagesLoaded( function() {
    //        $('#box-container').isotope({
    //            itemSelector: '.module-text-with-image-box',
    //            masonry: {
    //                columnWidth: 1,
    //                gutter: 0
    //            }
    //        });
    //    });
    //},

    /* responsive table */
    resptable: function(){
        $('.responsive-table').each(function(){
            var _this = $(this),
                ths = _this.find('thead th'),
                trs = _this.find('tbody tr'),
                thText = [];

            ths.each(function(i){
                thText[i] = $(this).text().trim();
            });

            trs.each(function(){
                tds = $(this).find('td');
                tds.each(function(i){
                    var tdtitle = thText[i] == undefined ? '' : thText[i];
                    $(this).prepend('<span class="td-title">' + tdtitle + '</span>');
                });
            });

            _this.parent('.tab-pane').addClass('table-in-tab');
        });
    },

    /* search layer */
    searchLayer: function(){
        $('#search-list').keyup(function(){
            var searchText = $(this).val();
            if(searchText.length >= 1){
                $(this).addClass('loading');
            }else{
                $(this).removeClass('loading');
                $(this).parent().parent().parent('form').parent().addClass('show-layer');
            }
            if(searchText.length >= 3){
                $('#search-list').parent().parent().parent('form').parent().addClass('show-layer');
                $('.module-search-layer-content').slideDown(function(){
                    $('#search-list').removeClass('loading');
                });
                $('.module-search-layer').show();
            }
        });
        $('.module-search-layer').click(function(){
            var _this = $(this);
            $('.module-search-layer-content').slideUp(function(){
                _this.hide();
                $(this).siblings().removeClass('show-layer');
            });
        });
    },

    /* profile flyout */
    profileFlyout: function(){
    	$('.top-login-box .dropdown-toggle').click(function(e){
    		e.stopPropagation();
    		$('.top-login-box .cart-flyout').hide();
    		$(this).toggleClass('open');
    		$(this).parent().find('.profile-flyout').slideToggle();
    		return false;
    	});
    	
    	$('.top-login-box .profile-flyout').click(function(e){
            e.stopPropagation();
        });

        $(document).click(function(){
            $('.top-login-box .profile-flyout').slideUp();
            $('.top-login-box .dropdown-toggle').removeClass('open');
        });
    },
    
    /* cart flyout */
    cartFlyout:function(){
        $('.top-login-box .cart-link').click(function(e){
            e.stopPropagation();
            $('.top-login-box .profile-flyout').hide();
            $('.top-login-box .dropdown-toggle').removeClass('open');
            $(this).parent().find('.cart-flyout').slideToggle();
            setTimeout(function(){
                var allSchollH = $('.cart-scroll').height();
                if(allSchollH<333){
                    return false;
                }else{
                    EG.cartscroll();
                    $('.cart-list-content .scroll-btn').show();
                }
            },100);
            return false;
        });

        $('.cart-content .title .arrow-up').click(function(){
            $(this).parents('.cart-flyout').slideToggle();
        });

        $('.top-login-box .cart-flyout').click(function(e){
            e.stopPropagation();
        });

        $(document).click(function(){
            $('.top-login-box .cart-flyout').slideUp();
        });

        $('.scroll-btn .scroll-up').click(function(){
            $(".cart-scroll").mCustomScrollbar("scrollTo",'+=100px')
        });
        $('.scroll-btn .scroll-down').click(function(){
            $(".cart-scroll").mCustomScrollbar("scrollTo",'-=100px')
        });
    },

    /* cart multiple scroll */
    cartscroll:function(){
        var scrolltop = 0;
        $('.scroll-btn .scroll-up').addClass('disabled');
        $(".cart-scroll").mCustomScrollbar({
            scrollButtons:{
                enable:true,
                scrollType:"pixels",
                scrollSpeed:60,
                scrollAmount:140
            },
            callbacks:{
                onScroll:function(){
                   scrollstatus(this);
                }
            }
        });

        function scrollstatus(el){
           scrolltop = el.mcs.top;
            var scrollpcct= el.mcs.topPct;
            if( scrolltop == 0){
                $('.scroll-btn .scroll-up').addClass('disabled');
            }else{
                $('.scroll-btn .scroll-up').removeClass('disabled');
            }
            if(scrollpcct == 100){
                $('.scroll-btn .scroll-down').addClass('disabled');
            }else{
                $('.scroll-btn .scroll-down').removeClass('disabled');
            }
        }
    },

    sameHeight: function(){
        var currentHeight = 0, finalHeight = 0;
        $('.same-height>div>div').each(function(){
            $(this).css('min-height', 'inherit');
            currentHeight = $(this).height();
            if(currentHeight>finalHeight){
                finalHeight = currentHeight;
            }
        });
        if($(window).width()>991&&finalHeight>0){
            $('.same-height>div>div').css('min-height', finalHeight);
        }
    },

	
	 /* max height */
    supportmax:function(){
        $('.teaser-multi-buttons-box').each(function(){
            var _this = $(this),
                columns = _this.find('.box');
            var boxge = function(columns){
                var  boxHeight= [];
                columns.each(function(i,o){
                    boxHeight[i] = $(this).height();
                });
                return boxHeight;
            };
            Array.prototype.max=function(){
                var i,max=this[0];
                for(i=1;i<this.length;i++){
                    if(max<this[i]){
                        max=this[i];
                    }
                }
                return max;
            };
            columns.css('height',boxge(columns).max());
            var spanheight = boxge(columns).max()-20;
            var innerH = columns.find('img').wrap('<i class="inner"></i>');
            columns.find('.inner').css('height',spanheight);
        });
    },		
	
	/* title view slider up */
    titleview:function(){
        if($(window).width() >= 768){
            $('.no-touch .filter-content-wrap .teaser-list li').hover(
                function(){
                    if($(this).children().hasClass('text-up')){
                        $(this).children('.text').fadeOut(150);
                        $(this).children('.text-up').slideDown(350);
                    }
                },
                function(){
                    if($(this).children().hasClass('text-up')){
                        $(this).children('.text-up').slideUp(250);
                        $(this).children('.text').fadeIn(350);
                    }
                });
        }
        $('.has-slider-layer .teaser-list li').each(function(){
           if($(this).children().hasClass('text-up')){
             $(this).addClass('has-textup');
           }
        });
    },

    /* cookie layer */
    cookieLayerHandle: function(){
        var layer = $('.cookie-layer');
        if(layer.length > 0 && layer.is(':visible')){
            $('.page-wrap').addClass('top-padding');  
            $('.cookie-layer .fancybox-close, .cookie-layer .btn-group .red-btn').click(function(){
                $('.cookie-layer').fadeOut(function(){
                    $('.cookie-overlay').hide();                  
                }); 
                $('.page-wrap').removeClass('top-padding');  
                return false;         
            });      
        } 
    },

    tabTextHandle: function(){
        $('.module-download-tabs .nav-tabs li a span').fitText(0.7, { minFontSize: '12px', maxFontSize: '19px' });
    },

    /* footer newsletter form validate */
    validateNewsletterForm: function(){
        function validateNewsletter() {
            var valid = true;
            $('#newsletter-form .required').each(function () {
                if ($(this).is(':text')) {
                    if ($(this).val() == '') {
                        valid = false;
                        $(this).addClass('error');
                    } else {
                        $(this).removeClass('error');
                        if ($(this).hasClass('email')) {
                            var reg = /^[\w!#$%&'*+/=?^_`{|}~-]+(?:\.[\w!#$%&'*+/=?^_`{|}~-]+)*@(?:[\w](?:[\w-]*[\w])?\.)+[\w](?:[\w-]*[\w])?$/;
                            if (!reg.test($(this).val())) {
                                valid = false;
                                $(this).addClass('error');
                            }else{
                                $(this).removeClass('error');
                            }
                        }
                    }
                }
            });
            return valid;
        }
        $('#newsletter-form button').click(function () {
            var valid = validateNewsletter();
            if (valid) {
                $('#newsletter-form .error-message').hide();
            } else {
                $('#newsletter-form .error-message').show();
                return false;
            }
        });
    },

    /* login form validate */
    validateLoginForm: function(){
        function validateLogin() {
            var valid = true;
            $('#login-form .required').each(function () {
                if ($(this).is(':text')) {
                    if ($(this).val() == '') {
                        valid = false;
                        $(this).addClass('error');
                    } else {
                        $(this).removeClass('error');
                        if ($(this).hasClass('email')) {
                            var reg = /^[\w!#$%&'*+/=?^_`{|}~-]+(?:\.[\w!#$%&'*+/=?^_`{|}~-]+)*@(?:[\w](?:[\w-]*[\w])?\.)+[\w](?:[\w-]*[\w])?$/;
                            if (!reg.test($(this).val())) {
                                valid = false;
                                $(this).addClass('error');
                            }else{
                                $(this).removeClass('error');
                            }
                        }
                    }
                }else if($(this).is(':password')){
                    if ($(this).val() == '') {
                        valid = false;
                        $(this).addClass('error');
                    }else{
                        $(this).removeClass('error');
                    }
                }
            });
            return valid;
        }
        $('#login-form button').click(function () {
            var valid = validateLogin();
            if (valid) {
                $('#login-form .error-message').hide();
            } else {
                $('#login-form .error-message').show();
                return false;
            }
        });
    },

    handleHeadline: function(){
        $('.rb-teaser-wrapper .modul.teaser.rb-teaser:odd').each(function(){
            var _this = $(this),
                _prev = _this.prev('.modul.teaser.rb-teaser'),
                teasers = _this.add(_prev);
   
            for(var i=0; i<2; i++){
                var headlines = teasers.map(function(){
                    return $(this).find('h2').eq(i);
                });
                var maxHeight = Math.max.apply(Math, headlines.map(function(){
                    return $(this).css('height', 'auto').height();
                }).get());
                headlines.each(function(){
                    $(this).height(maxHeight).wrapInner('<span></span>');
                });
            }
        });
    },

    treeViewFilter: function(){
        $('.filter-box').treeFilter();
    },

    handleDownloadFilter: function () {
        $('.module-product-tab .tab-pane').downloadFilter();
    },

    colorSlider:function () {
        if($(window).width()>767){
            var item = 4;
            $('.slider-color .slider-color-list').each(function () {
                item = $(this).attr('data');
                $(this).bxSlider({
                    mode: 'vertical',
                    infiniteLoop:false,
                    pager:false,
                    hideControlOnEnd:true,
                    minSlides: item,
                    maxSlides: 4,
                    slideWidth: 65,
                    slideMargin: 10,onSlideNext:function () {
                        $('.caption-layer li').hide();
                    },
                    onSlidePrev:function () {
                        $('.caption-layer li').hide();
                    }
                });
            });
            var again = false;
            $('.fixed-btn .layer-btn').click(function (e) {
                e.stopPropagation();
                if(again){
                    if($(this).hasClass('show')){
                        return false;
                    }else{
                        $(this).addClass('show').siblings().removeClass('show');
                        $(this).parent().parent().siblings('.big-img-layer').css({left:0,top:'-100%'}).animate({top: 0 }, 300);
                        $(this).parent().parent().siblings('.slider-color').animate({ top: '100%'}, 400 );
                        $('.caption-layer li').hide();
                        again = true;
                    }
                }else{
                    if($(this).siblings('.slider-btn').hasClass('show')){
                        $(this).addClass('show').siblings().removeClass('show');
                        $(this).parent().parent().siblings('.slider-color').animate({ top: '100%'}, 400 );
                        $(this).parent().parent().siblings('.big-img-layer').animate({ top: 0 }, 300 );
                        $('.caption-layer li').hide();
                        again = true;
                    }else{
                        $(this).addClass('show').siblings().removeClass('show');
                        $(this).parent().parent().siblings('.big-img-layer').animate({top: 0 }, 300 );
                    }
                }
                return false;
            });
            $('.fixed-btn .slider-btn').click(function (e) {
                e.stopPropagation();
                if(again){
                    if($(this).hasClass('show')){
                        return false;
                    }else{
                        $(this).addClass('show').siblings().removeClass('show');
                        if($(this).parent().parent().siblings('.slider-color').hasClass('center')){
                            $(this).parent().parent().siblings('.slider-color').css({left:'120%',top:10}).animate({left: '50%' }, 500 );
                        }else{
                            $(this).parent().parent().siblings('.slider-color').css({left:'100%',top:10}).animate({left: 45 }, 500 );
                        }

                        $(this).parent().parent().siblings('.big-img-layer').animate({ left: '-100%' },300);
                        again = true;
                    }
                }else{
                    if($(this).siblings('.layer-btn').hasClass('show')){
                        $(this).addClass('show').siblings().removeClass('show');
                        $(this).parent().parent().siblings('.big-img-layer').animate({ left: '-100%' },300);
                        if($(this).parent().parent().siblings('.slider-color').hasClass('center')){
                            $(this).parent().parent().siblings('.slider-color').animate({left: '50%' }, 500 );
                        }else{
                            $(this).parent().parent().siblings('.slider-color').animate({left: 45 }, 500 );
                        }
                        again = true;
                    }else{
                        $(this).addClass('show').siblings().removeClass('show');
                        if($(this).parent().parent().siblings('.slider-color').hasClass('center')){
                            $(this).parent().parent().siblings('.slider-color').animate({left: '50%' }, 500 );
                        }else{
                            $(this).parent().parent().siblings('.slider-color').animate({left: 45 }, 500 );
                        }
                    }
                }
                return false;
            });
            /* hover show layer */
            if(EG.isTouchDevice()){
                $('.set-image-box .slider-color .bx-viewport li').each(function (i) {
                    $(this).bind('click',function() {
                        $(this).parent().parent().parent().parent('.slider-color').siblings('.caption-layer').children('li').eq(i).show().offset($(this).offset()).siblings().hide();
                    });
                });
                $('.inline-slider-overlay .slider-color .bx-viewport li').each(function (i) {
                    $(this).bind('click',function() {
                        $(this).parent().parent().parent().parent('.slider-color').siblings('.caption-layer').children('li').eq(i).show().offset($(this).offset()).siblings().hide();
                    });
                });
                $('.caption-layer li').bind('click',function () {
                    $(this).hide();
                    return false;
                });
            }else{
                $('.set-image-box .slider-color .bx-viewport li').each(function (i) {
                    $(this).hover(function() {
                        $(this).parent().parent().parent().parent('.slider-color').siblings('.caption-layer').children('li').eq(i).show().offset($(this).offset());
                    });
                });
                $('.inline-slider-overlay .slider-color .bx-viewport li').each(function (i) {
                    $(this).hover(function() {
                        $(this).parent().parent().parent().parent('.slider-color').siblings('.caption-layer').children('li').eq(i).show().offset($(this).offset());
                    });
                });
                $('.caption-layer li').mouseleave(function () {
                    $(this).hide();
                });
            }
        }else{
            $('.slider-color .slider-color-list').bxSlider({
                mode: 'horizontal',
                infiniteLoop:false,
                pager:false,
                hideControlOnEnd:true,
                minSlides: 4,
                maxSlides: 4,
                slideWidth: 45,
                slideMargin: 10,
                onSlideNext:function () {
                    $('.caption-layer li').hide();
                },
                onSlidePrev:function () {
                    $('.caption-layer li').hide();
                }
            });
            var again = false;
            $('.fixed-btn .layer-btn').click(function (e) {
                e.stopPropagation();
                if(again){
                    if($(this).hasClass('show')){
                        return false;
                    }else{
                        $(this).addClass('show').siblings().removeClass('show');
                        $(this).parent().parent().siblings('.big-img-layer').css({left:0,top:-176}).animate({top: 0 }, 300);
                        $(this).parent().parent().siblings('.slider-color').animate({ top: '-100%'}, 400 );
                        $('.caption-layer li').hide();
                        again = true;
                    }
                }else{
                    if($(this).siblings('.slider-btn').hasClass('show')){
                        $(this).addClass('show').siblings().removeClass('show');
                        $(this).parent().parent().siblings('.slider-color').animate({ top: '-100%'}, 400 );
                        $(this).parent().parent().siblings('.big-img-layer').animate({ top: 0 }, 300 );
                        $('.caption-layer li').hide();
                        again = true;
                    }else{
                        $(this).addClass('show').siblings().removeClass('show');
                        $(this).parent().parent().siblings('.big-img-layer').animate({top: 0 }, 300 );
                    }
                }
                return false;
            });
            $('.fixed-btn .slider-btn').click(function (e) {
                e.stopPropagation();
                if(again){
                    if($(this).hasClass('show')){
                        return false;
                    }else{
                        $(this).addClass('show').siblings().removeClass('show');
                        $(this).parent().parent().siblings('.slider-color').css({top:'-100%'}).animate({top: 0 }, 500 );
                        $(this).parent().parent().siblings('.big-img-layer').animate({ top: '-100%' },300);
                        again = true;
                    }
                }else{
                    if($(this).siblings('.layer-btn').hasClass('show')){
                        $(this).addClass('show').siblings().removeClass('show');
                        $(this).parent().parent().siblings('.big-img-layer').animate({ top: '-100%' },300);
                        $(this).parent().parent().siblings('.slider-color').animate({top: 0 }, 500 );
                        again = true;
                    }else{
                        $(this).addClass('show').siblings().removeClass('show');
                        $(this).parent().parent().siblings('.slider-color').animate({top: 0 }, 500 );
                    }
                }
                return false;
            });
            if(EG.isTouchDevice()){
                /* hover show layer */
                $('.slider-color .bx-viewport li').each(function (i) {
                    $(this).bind('click',function() {
                        $(this).parent().parent().parent().parent('.slider-color').siblings('.caption-layer').children('li').eq(i).show();
                        $(this).parent().parent().siblings('.bx-controls').hide();
                        $(this).parents().find('.set-image-box').children('.bx-wrapper').addClass('disable');
                    });
                });

                $('.caption-layer li').bind('click',function () {
                    $(this).hide();
                    $(this).parent().siblings('.slider-color').children('.bx-wrapper').children('.bx-controls').show();
                    $(this).parents().find('.set-image-box').children('.bx-wrapper').removeClass('disable');
                });
            }else{
                /* hover show layer */
                $('.slider-color .bx-viewport li').each(function (i) {
                    $(this).hover(function() {
                        $(this).parent().parent().parent().parent('.slider-color').siblings('.caption-layer').children('li').eq(i).show();
                        $(this).parent().parent().siblings('.bx-controls').hide();
                        $(this).parents().find('.set-image-box').children('.bx-wrapper').addClass('disable');
                    });
                });

                $('.caption-layer li').mouseleave(function () {
                    $(this).hide();
                    $(this).parent().siblings('.slider-color').children('.bx-wrapper').children('.bx-controls').show();
                    $(this).parents().find('.set-image-box').children('.bx-wrapper').removeClass('disable');
                });
            }
        }
    },

    checkCode:function () {
        $('.form-group .check-code').click(function () {
            var codeText = $(this).siblings('.code').children('.form-control').val();
            if(codeText.length >= 1){
                $(this).siblings('.success-info').show().siblings('.error-info').hide();
                $(this).siblings('.code').children('.form-control').removeClass('error').siblings('.check').show();
            }
        });
    },
    support:function () {
        $(".module-de-gmap .gray-box>.title>.plus-icon").click(function(){
            $(this).parents('.title').toggleClass('open');
            $(this).parents('.gray-box').find('.control-item').slideToggle();
        });
        $(".col-1 .form-group>label").click(function(){
            var c=$(this).siblings().find('label');
                c.removeClass('checked');
                c.find("input[type='checkbox']").removeAttr("checked");
        });
    },

    topAlert: {
        init: function () {
            var self = this;
            $('.top-alert .btn-close, .top-alert .btn-cancel').click(function () {
                self.close();
                return false;
            });
        },
        open: function () {
            $('.top-alert').slideDown();
        },
        close: function () {
            $('.top-alert').slideUp();
        }
    },

    /* Multi-line text overflow */
    textOverflow:function () {
        Ellipsis({
        	class: '.module-product-muster .teaser-list .text h4, .modul.teaser figcaption h3',
            debounce: 100,
            lines: 3
        });
    }

};

!function ($) {

    $.extend( $.easing, {
        easeOutCubic: function (x, t, b, c, d) {
            return c*((t=t/d-1)*t*t + 1) + b;
        },
        easeInExpo: function (x, t, b, c, d) {
            return (t==0) ? b : c * Math.pow(2, 10 * (t/d - 1)) + b;
        },
        easeOutExpo: function (x, t, b, c, d) {
            return (t==d) ? b+c : c * (-Math.pow(2, -10 * t/d) + 1) + b;
        }
    });
    
    $.fn.radios = function(options){
        var defaults = {
            checkedClass: 'checked'
        };
        var opts = $.extend(defaults, options);
        return this.each(function(){
            var el = $(this),
                radioGroup = {};

            radioGroup[el.attr('name')] = el.attr('name');

            for(var i in radioGroup){
                $('input[name="'+ radioGroup[i] +'"]').each(function(){
                    if($(this).is(":checked")){
                        $(this).parent().addClass(opts.checkedClass);
                    }
                    $(this).parent().click(function(){
                        $(this).addClass(opts.checkedClass).find('input:radio').attr('checked',true).end().siblings().removeClass(opts.checkedClass);
                    });
                });
            }

        });
    };

    $.fn.checkbox = function(options){
        var defaults = {
            checkedClass: 'checked'
        };
        var opts = $.extend(defaults, options);
        return this.each(function(){
            var el = $(this);

            var init = function(){
				if(el.find(':checkbox').length) {
					var isCheck = el.find(':checkbox').is(":checked");
					if(isCheck){
						el.addClass(opts.checkedClass);
					}else{
						el.removeClass(opts.checkedClass);
					}
				}
                el.click(function(e){
                    var $checker = el.find(':checkbox'),
                        isChecked = $checker.is(":checked");
                    if ($checker.prop) {
                        $checker.prop("checked", isChecked);
                    } else {
                        if (isChecked) {
                            $checker.attr("checked", "checked");
                        } else {
                            $checker.removeAttr("checked");
                        }
                    }
                    if(isChecked){
                        el.addClass(opts.checkedClass);
                    }else{
                        el.removeClass(opts.checkedClass);
                    }
                });
            };

            init();
        });
    };

    $.fn.thumbSlider = function(options){
        var defaults = {
            sliderMain : '.slider-main',
            thumbnail: '.thumbnails'
        };
        var options = $.extend(defaults, options);
        return this.each(function(i){
            var el = $(this);
            var slider = {};

            var init = function(){
                slider.mainSlider = el.find(options.sliderMain);
                slider.thumbnail = el.find(options.thumbnail);

                slider.mainSlider.bxSlider({
                    controls: true,
                    pagerCustom: slider.thumbnail,
                    onSliderLoad: function(currentIndex){
                        var imgHeight = slider.mainSlider.children('li').not('.bx-clone').eq(currentIndex).find('img').height(),
                            captionHeight = slider.mainSlider.children('li').not('.bx-clone').eq(currentIndex).find('.caption').height();
                        el.find('.bx-prev').css('top', imgHeight/2);
                        el.find('.bx-next').css('top', imgHeight/2);
                        slider.thumbnail.css('bottom', captionHeight + 15);
                    },
                    onSlideAfter: function($slideElement, oldIndex, newIndex){
                        var imgHeight = $slideElement.find('img').height(),
                            captionHeight = $slideElement.find('.caption').height();
                        el.find('.bx-prev').css('top', imgHeight/2);
                        el.find('.bx-next').css('top', imgHeight/2);
                        slider.thumbnail.css('bottom', captionHeight + 15);
                    }
                });
            };
            init();
        });
    };

    $.fn.imgGallerySlider = function(options){
        var defaults = {
            sliderMain : '.slider-main',
            sliderThumb: '.slider-thumb',
            zoomIcon: '.view'
        };
        var options = $.extend(defaults, options);
        return this.each(function(i){
            var el = $(this);
            var slider = {};

            var init = function(){
                slider.mainSlider = el.find(options.sliderMain);
                slider.thumbSlider = el.find(options.sliderThumb);
                slider.iconZoom = el.find(options.zoomIcon);

                slider.mainSlider.bxSlider();
                slider.thumbSlider.bxSlider({
                    minSlides: 2,
                    maxSlides: 4,
                    slideWidth: 250,
                    slideMargin: 10,
                    moveSlides: 1,
                    pager: false,
                    infiniteLoop: false,
                    hideControlOnEnd: true
                });
                slider.thumbSlider.find('a').click(function(){
                    var i = $(this).data('slide-index');
                    $(this).addClass('active').parent().siblings().find('a').removeClass('active');
                    slider.mainSlider.goToSlide(i);
                    //slider.thumbSlider.goToSlide(i);
                    return false;
                });
                if(slider.iconZoom.length > 0){
                    slider.iconZoom.click(function(){
                        var current = slider.mainSlider.getCurrentSlide();
                        el.find('li').not('.bx-clone').eq(current).find('a').trigger('click');
                    });
                }
            };
            init();
        });
    };
    
    $.fn.multiLayerSlider = function(options){
        var defaults = {
        	selecter : '.zaccordion'
        };
        var options = $.extend(defaults, options);
        return this.each(function(i){
            var el = $(this);
            var obj = {};

            var init = function(){
            	obj.slideEl = el.find(options.selecter).length > 0 ? el.find(options.selecter) : el.children('ul');
            	obj.slideElCount = obj.slideEl.find('li').length;
            	obj.accordion = null;
            	obj.slider = null;
            	
            	if($(window).width() < 767){
                    if(obj.accordion != null){
                    	obj.accordion.destroy();
                    	obj.accordion = null;
                    }                  
                    obj.slider = obj.slideEl.bxSlider({infiniteLoop : false});
                    if(obj.slideElCount == 1){
                    	el.addClass('single-slider');
                    }                   
                }else{
                    if(obj.slider != null){
                    	obj.slider.destroySlider();
                    	obj.slider = null;
                    }
                    obj.accordion = obj.slideEl.zAccordion({
                        tabWidth: "7%",
                        width: "100%",
                        height: "100%",
                        slideClass: 'slide',
                        invert: true,
                        //easing: 'easeInExpo',
                        speed: 600,
                        auto: false,
                        animationStart: function () {
                        	obj.slideEl.find('li.slide-previous .caption h4').fadeOut();
                        },
                        animationComplete: function () {
                        	obj.slideEl.find('li.slide-open .caption h4').fadeIn();
                        },
                        buildComplete: function () {
                        	obj.slideEl.find('li.slide-closed .caption h4').hide();
                        	obj.slideEl.find('li.slide-open .caption h4').fadeIn();
                        }
                    });
                    if(EG.isTouchDevice()){
                        if(obj.slideEl){
                            var mc = new Hammer(obj.slideEl[0]);
                            var num = obj.slideEl.find('li').length;
                            mc.on("swipeleft", function() {
                            	obj.slideEl.zAccordion("trigger", obj.slideEl.find('li.slide-open').index() + 1 >= (num -1) ? (num -1) : obj.slideEl.find('li.slide-open').index() + 1);
                                return false;
                            });
                            mc.on("swiperight", function() {
                            	obj.slideEl.zAccordion("trigger", obj.slideEl.find('li.slide-open').index() - 1 <= 0 ? 0 : obj.slideEl.find('li.slide-open').index() - 1);
                                return false;
                            });
                        }
                    }
                }
            };
            init();
        });
    };

    $.fn.carousel3D = function(options){
        var defaults = {
            imgList : '.carousel',
            textList: '.image-text'
        };
        var options = $.extend(defaults, options);
        return this.each(function(i){
            var el = $(this);
            var carousel = {};

            var init = function(){
                carousel.imgList = el.find(options.imgList);
                carousel.textList = el.find(options.textList);
                carousel.length = carousel.imgList.length;
                carousel.imgSlider = null;
                carousel.textSlider = null;
                carousel.textUl = '';

                creatTextList(carousel.imgList);

                carousel.textSlider = carousel.textList.find('ul').bxSlider({
                    pager: false,
                    controls: false
                });

                if($(window).width() < 767){
                    carousel.imgSlider = carousel.imgList.find('.slides').bxSlider({
                        onSlideNext: function ($slideElement, oldIndex, newIndex) {
                            carousel.textSlider.goToNextSlide();
                        },
                        onSlidePrev: function ($slideElement, oldIndex, newIndex) {
                            carousel.textSlider.goToPrevSlide();
                        }
                    });
                }else{
                    carousel.imgList.carousel({
                        directionNav:true,
                        speed:500,
                        carouselWidth:390,
                        carouselHeight:200,
                        frontWidth:259,
                        frontHeight:177,
                        hAlign:'left',
                        vAlign:'center',
                        autoplay:false,
                        autoplayInterval:5000,
                        shadow:false,
                        reflection:false,
                        reflectionHeight:0.2,
                        reflectionOpacity:0.4,
                        description:false,
                        mouse: false,
                        descriptionContainer:'.carousel_description'
                    });
                }

                carousel.imgList.on('click', '.next', function () {
                    carousel.textSlider.goToNextSlide();
                });

                carousel.imgList.on('click', '.prev', function () {
                    carousel.textSlider.goToPrevSlide();
                });

                carousel.imgList.on('click', '.bx-pager .bx-pager-link', function () {
                    var index = $(this).data('slide-index');
                    carousel.textSlider.goToSlide(index);
                });

                carousel.imgList.find('li').click(function () {
                    var index = $(this).index();
                    carousel.textSlider.goToSlide(index);
                });
            };
            
            var creatTextList = function (imglist) {
                var listItems = '';
                imglist.find('li').each(function () {
                    listItems += '<li>' + $(this).find('.description').html() + '</li>';

                });
                carousel.textUl = '<ul>' + listItems + '</ul>';
                carousel.textList.append(carousel.textUl);
            };
            
            init();
        });
    };

    $.fn.treeFilter = function(options){
        var defaults = {
            treeEl : '.jstree',
            moreBtn: '.more-style',
            lessBtn: '.less-style',
            resetBtn: '.reset'
        };
        var options = $.extend(defaults, options);
        return this.each(function(i){
            var el = $(this);
            var Filter = {};

            var init = function(){
                Filter.treeEl = el.find(options.treeEl);
                Filter.moreBtn = el.find(options.moreBtn);
                Filter.checkboxs = el.find('.checkbox>:checkbox');
                Filter.lessBtn = el.find(options.lessBtn);
                Filter.resetBtn = el.find(options.resetBtn);
                Filter.checkedLength = 0;

                expandFilter();
                if(checkedNum()>0 || treeCheckedNum()>0){
                    Filter.resetBtn.addClass('active');
                }

                $.jstree.defaults.core.dblclick_toggle = false;
                $.jstree.defaults.core.expand_selected_onload = true;
                Filter.treeEl.jstree({
                    "core" : {
                        "themes" : {
                            "dots" : false,
                            "icons" : false
                        }
                    },
                    "plugins" : ["checkbox"],
                    "checkbox" : {
                        "three_state" : false,
                        "two_state" : true
                    }
                });

                Filter.treeEl.find('>ul>li>a').click(function(e){
                    e.stopPropagation();
                    $(this).prev('.jstree-ocl').trigger('click');
                    return false;
                });

                Filter.treeEl.each(function(){
                    var _this = $(this);
                    _this.on('changed.jstree', function (e, data) {
                        if(data.action == 'select_node' || data.action == 'deselect_node'){
                            var rel = data.node.data.id;
                            if (data.node.state.selected) {
                                el.find("[data-rel=" + rel + "]").slideDown();
                            } else {
                                el.find("[data-rel=" + rel + "]").slideUp();
                            }
                        }
                        if (data.selected.length > 0) {
                            Filter.resetBtn.addClass('active');
                        } else {
                            if(checkedNum() == 0 && treeCheckedNum() == 0){
                                Filter.resetBtn.removeClass('active');
                            }
                        }
                    });
                });

                Filter.moreBtn.click(function(){
                    $(this).hide();
                    Filter.lessBtn.css('display', 'inline-block');
                    el.find('.more-filter').slideDown();
                    return false;
                });

                Filter.lessBtn.click(function(){
                    $(this).hide();
                    Filter.moreBtn.css('display', 'inline-block');
                    el.find('.more-filter').slideUp();
                    return false;
                });

                Filter.resetBtn.click(function(){
                    Filter.treeEl.each(function () {
                        $(this).jstree(true).deselect_all();
                    });
                    el.find('.addtional-filter').hide();
                    return false;
                });

                Filter.checkboxs.change(function () {
                    var rel = $(this).parent().data('id'),
                        isChecked = $(this).is(":checked");
                    if(isChecked){
                        el.find("[data-rel=" + rel + "]").slideDown();
                    }else{
                        el.find("[data-rel=" + rel + "]").slideUp();
                    }
                });
            };

            var expandFilter = function () {
                el.find('.gray-box').each(function () {
                    var _this = $(this),
                        checkedNum = $(this).find(':checkbox').filter(":checked").length;
                    if(checkedNum > 0){
                        _this.find('.title').addClass('open');
                        _this.find('.control-item').show();
                    }
                });
            };

            var checkedNum = function () {
                return Filter.checkboxs.filter(":checked").length;
            };

            var treeCheckedNum = function () {
                var num = 0;
                Filter.treeEl.each(function () {
                    num += $(this).jstree("get_checked",null,true).length;
                });
                return num;
            };

            init();
        });
    };

    $.fn.downloadFilter = function(options){
        var defaults = {
            typeSelect : '.select-type',
            langSelect: '.select-lang',
            downloadItem: '.download-item'
        };
        var opts = $.extend(defaults, options);
        return this.each(function(i){
            var el = $(this);
            var Filter = {};

            var init = function(){
                Filter.typeSelect = el.find(opts.typeSelect);
                Filter.langSelect = el.find(opts.langSelect);
                Filter.downloadItem = el.find(opts.downloadItem);
                Filter.typeArr = [];
                Filter.langArr = [];
                Filter.defaultType = Filter.typeSelect.find('option').eq(0).attr('value');
                Filter.defaultLang = Filter.langSelect.find('option').eq(0).attr('value');

                $('ul li', Filter.downloadItem).each(function () {
                    var type = $(this).data('doctype'),
                        lang = $(this).data('language');
                    if(Filter.typeArr.indexOf(type) == -1){
                        Filter.typeArr.push(type);
                    }
                    if(Filter.langArr.indexOf(lang) == -1){
                        Filter.langArr.push(lang);
                    }
                });
                $('option:gt(0)', Filter.typeSelect).remove();
                $('option:gt(0)', Filter.langSelect).remove();
                for(var i=0; i<Filter.typeArr.length; i++){
                    Filter.typeSelect.append('<option value="'+ Filter.typeArr[i] +'">'+ Filter.typeArr[i] +'</option>');
                }
                for(var j=0; j<Filter.langArr.length; j++){
                    Filter.langSelect.append('<option value="'+ Filter.langArr[j] +'">'+ Filter.langArr[j] +'</option>');
                }
                Filter.typeSelect.selectpicker('refresh');
                Filter.langSelect.selectpicker('refresh');
                Filter.typeSelect.change(function () {
                    var type = $(this).val(),
                        lang = Filter.langSelect.val();
                    filter(type, lang);
                });
                Filter.langSelect.change(function () {
                    var lang = $(this).val(),
                        type = Filter.typeSelect.val();
                    filter(type, lang);
                });
            };

            var filter = function(t, l) {
                if(t == Filter.defaultType && l == Filter.defaultLang){
                    Filter.downloadItem.show();
                    $('ul li', Filter.downloadItem).show();
                }else{
                    $('ul li', Filter.downloadItem).each(function () {
                        var type = $(this).data('doctype'),
                            lang = $(this).data('language');
                        if(type== t && lang == l || l == Filter.defaultLang && type == t || t == Filter.defaultType && lang == l){
                            $(this).parent().parent().show();
                            $(this).show();
                        }else{
                            $(this).hide();
                        }
                    });
                }
                handleHeadline();
            };

            var handleHeadline = function() {
                Filter.downloadItem.each(function () {
                    var _this = $(this),
                        items = _this.find('li'),
                        isAllHide = true;
                    items.each(function () {
                        if($(this).is(':visible')){
                            isAllHide = false;
                            return;
                        }
                    });
                    if(isAllHide){
                        _this.hide();
                    }else{
                        _this.show();
                    }
                })
            };

            init();
        });
    };


    $(document).ready(function(){
        EG.pageMark();
        EG.stickyHeader();
        EG.radioHandle();
        EG.checkHandle();
        EG.navToggle();
        EG.footerMenuToggle();
        EG.handlePartnerSearch();

        EG.topAlert.init();
        EG.textOverflow();

        EG.togglePanel();
        EG.viewToggle();

        EG.lightbox();
        EG.viewiframe();
        EG.videolayer();
        EG.imgView();
        EG.imgGallery();
        EG.verification();

        EG.imgHover();
        EG.bannerSlider();
        EG.imageSlider();
        EG.imgSetSlider();
        EG.productSlider();
        EG.thumbSlider();
        EG.imgGallerySlider();
        EG.multiLayerSlider();
        EG.rbslider();
        EG.imgCarousel();
        EG.flexslider();
        EG.layerslider();
        EG.newsletterclose();
        EG.rbaccordion();
        EG.filterHandle();
        EG.calendershow();

        //EG.inputAutoComplete();
        EG.searchLayer();
        EG.placeholderSupport();
        EG.checkboxChecked();

        //EG.mobileNavHandle();

        EG.tabNavSlider();
        EG.productTableHandle();
        EG.panelExpand();
        EG.stickyPanel();

        EG.filterSelect();
        EG.filterReset();
        EG.sideNav();
        EG.activeBtn();
        EG.videoLightbox();

        //EG.waterfall();
        EG.resptable();

        EG.tooltip();
        EG.tooltips();
        EG.errorTips();
        EG.profileFlyout();
        EG.cartFlyout();
		EG.supportmax();
		EG.titleview();

        EG.sameHeight();
        EG.cookieLayerHandle();
        EG.tabTextHandle();

        EG.validateNewsletterForm();
        EG.validateLoginForm();

        EG.handleHeadline();
        EG.producttable();

        EG.treeViewFilter();
        EG.handleDownloadFilter();

        EG.colorSlider();
        EG.checkCode();
        EG.support();

        $(window).resize(function(){
            EG.headerHeight = $('.header-top').outerHeight();
            EG.footerMenuToggle();
            EG.tabNavSlider();
            EG.productTableHandle();
            EG.tagPosition();
            EG.filterSelect();
            EG.calendershow();
            //EG.imageSlider();
            EG.sameHeight();
        });

    });

    $(window).load(function(){
        EG.gototop();
        EG.calenderdropdown();
        if(EG.isTouchDevice){
            EG.mobileNavHandle();
        }
    });

}(window.jQuery);



