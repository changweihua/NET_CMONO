var _DENSITY = 20;
var _FPS = 30;
var _OPACITY = 0.50;
(function($) {
    $.fn.effect = function(opts) {
        var element = this;
        $(element).children().each(function(index) {
            var that = this;
            var opacity = Math.round((1 / $(that).offset().top) * ($(window).height() / 2 + $(window).scrollTop()) * 100) / 100;
            $(that).find(".effect").css({
                "opacity": opacity >= 1 ? 1 : 0
            });
        });
        return (this);
    };
    $.fn.engine = function(opts) {
        var element = this;
        var density = _DENSITY;
        var fps = _FPS;
        var ratio = Math.round(window.devicePixelRatio ? window.devicePixelRatio : 1);
        var stack = [];
        var cvs = $(element).find(".animation").get(0);
        var ctx = cvs.getContext("2d");
        //var img = $("<img/>").attr("src", "images/arrow.png");
        var count = 0;
        var fn = function() {
            cvs.width = $(element).width() * ratio;
            cvs.height = $(element).height() * ratio;
            ctx.clearRect(0, 0, cvs.width, cvs.height);
            // if (++count % fps <= fps / 2) {
            //     ctx.drawImage($(img).get(0), (cvs.width - (cvs.height / 8 <= 100 * ratio ? cvs.height / 8 : 100 * ratio)) / 2, (cvs.height - (cvs.height / 8 <= 100 * ratio ? cvs.height / 8 : 100 * ratio)) / 1, (cvs.height / 8 <= 100 * ratio ? cvs.height / 8 : 100 * ratio), (cvs.height / 8 <= 100 * ratio ? cvs.height / 8 : 100 * ratio));
            // }
            $(stack).each(function(index) {
                var obj = this;
                var x = _OPACITY - _OPACITY / (cvs.width / 2) * Math.abs(cvs.width / 2 - obj.position.x);
                var y = _OPACITY - _OPACITY / (cvs.height / 2) * Math.abs(cvs.height / 2 - obj.position.y);
                ctx.beginPath();
                ctx.arc(obj.position.x, obj.position.y, obj.speed.x * obj.speed.y % (50 * ratio) + (10 * ratio), (0 * Math.PI), (2 * Math.PI));
                ctx.globalAlpha = x <= y ? x : y;
                ctx.fillStyle = "rgba(" + obj.color.r + ", " + obj.color.g + ", " + obj.color.b + ", 1)";
                ctx.fill();
                ctx.closePath();
                obj.position.x = ((obj.speed.x % 2 ? obj.position.x + obj.speed.x / fps : obj.position.x - obj.speed.x / fps) + cvs.width) % cvs.width;
                obj.position.y = ((obj.speed.y % 2 ? obj.position.y + obj.speed.y / fps : obj.position.y - obj.speed.y / fps) + cvs.height) % cvs.height;
            });
        };
        while (density--) {
            stack.push({
                "position": {
                    "x": Math.round(Math.random() * 1000000) % ($(element).width() * ratio),
                    "y": Math.round(Math.random() * 1000000) % ($(element).height() * ratio)
                },
                "speed": {
                    "x": Math.round(Math.random() * 1000000) % (50 * ratio),
                    "y": Math.round(Math.random() * 1000000) % (50 * ratio)
                },
                "color": {
                    "r": Math.round(Math.random() * 1000000) % 64 + 192,
                    "g": Math.round(Math.random() * 1000000) % 64 + 192,
                    "b": Math.round(Math.random() * 1000000) % 64 + 192
                }
            });
        }
        fn();
        setInterval(function() {
            fn();
        }, 1000 / fps);
        $(window).bind("load resize scroll", function(event) {
            var opacity = Math.round((1 - 2 / $(element).height() * $(window).scrollTop()) * 100) / 100;
            var margin = Math.round($(element).height() / 2 - $(element).find(".content").height() / 2 - $(window).scrollTop() / 4);
            // $(element).find(".content").css({
            //     "margin": margin + "px auto 0 auto",
            //     "opacity": opacity >= 0 ? opacity : 0
            // });
            $(element).find(".animation").css({
                "opacity": opacity >= 0 ? opacity : 0
            });
        });
        return (this);
    };
})(jQuery);