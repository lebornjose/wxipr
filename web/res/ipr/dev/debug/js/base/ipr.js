 (function(document) {
        var toggle = document.querySelector('.sidebar-toggle');
        var sidebar = document.querySelector('#sidebar');
        var checkbox = document.querySelector('#sidebar-checkbox');

        document.addEventListener('click', function(e) {
          var toggle = document.querySelector('.sidebar-toggle');
          var sidebar = document.querySelector('#sidebar');
          var checkbox = document.querySelector('#sidebar-checkbox');
          var target = e.target;

          if(!checkbox.checked ||sidebar.contains(target) || (target === checkbox || target === toggle)) 
            return;
          checkbox.checked = false;
        }, false);
      })(document);
      
      (function(i,s,o,g,r,a,m){i['GoogleAnalyticsObject']=r;i[r]=i[r]||function(){
        (i[r].q=i[r].q||[]).push(arguments)},i[r].l=1*new Date();a=s.createElement(o),
        m=s.getElementsByTagName(o)[0];a.async=1;a.src=g;m.parentNode.insertBefore(a,m)
      })(window,document,'script','/res/ipr/dev/debug/js/base/analytics.js','ga');

      ga('create', 'UA-5531632-1', 'auto');
      ga('send', 'pageview');
