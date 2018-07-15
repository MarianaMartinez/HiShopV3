


function InitialiseFacebook(appId) {

    window.fbAsyncInit = function () {
        FB.init({
            appId: appId,
            status: true,
            cookie: true,
            xfbml: true
        });

        FB.Event.subscribe('auth.login', function (response) {
            var credentials = { uid: response.authResponse.userID, accessToken: response.authResponse.accessToken };
            SubmitLogin(credentials);
        });

        FB.getLoginStatus(function (response) {
            $.ajax({
                async: false,
                type: "GET"
            });
            if (response.status === 'connected') {
               
                manejarElEstadoConected(response);

            }

        });

        function manejarElEstadoConected(response) {
            var url = "https://graph.facebook.com/me?access_token=" + response.authResponse.accessToken;
            var faceJson = $.getJSON(url, function (respuestaFacebook) {
                console.log("Se trajo el usuario : " + respuestaFacebook.name);
                var nombre = respuestaFacebook.name;
                if (nombre != "" && nombre != null && nombre != "undefined") {
                    loguearConFacebook(respuestaFacebook);

                }
               });
         }
        function SubmitLogin(credentials) {
            $.ajax({
                url: "/account/facebooklogin",
                type: "POST",
                data: credentials,
                error: function () {
                    alert("error logging in to your facebook account.");
                },
                success: function () {
                    window.location.reload();
                }
            });
        }

    };

    (function (d) {
        var js, id = 'facebook-jssdk', ref = d.getElementsByTagName('script')[0];
        if (d.getElementById(id)) {
            return;
        }
        js = d.createElement('script');
        js.id = id;
        js.async = true;
        js.src = "//connect.facebook.net/en_US/all.js";
        ref.parentNode.insertBefore(js, ref);
    }(document));
}




function cerrarSessionFacebook() {

    $(function () {
        var data = sessionStorage.getItem('accesToken');
    });
    fb.logout();
   window.location.href = "/usuario/cerrarsesion";


}

function loguearConFacebook(respuestaFacebook) {

    var nombreUsuario = respuestaFacebook.name;
    var idUsuario = respuestaFacebook.id;
    var urlActual = window.location.origin;
    $.ajax({
        url: '/FaceController/loguearConFacebook',
        async: false,
        data: {
            nombreUsuario, idUsuario, urlActual
        },
        type: 'POST',
        success: (function (respuesta) {
            debugger;
            if (respuesta == "/Usuario/Perfil") {
                window.location.href = respuesta;
            }
        }),
        error: (function (result) {
            alert("Error al ingresar con Facebook .");
        }),
    })
}


function cargarFacebook() {
    debugger;
    (function (d) {
        var js, id = 'facebook-jssdk', ref = d.getElementsByTagName('script')[0];
        if (d.getElementById(id)) {
            return;
        }
        js = d.createElement('script');
        js.id = id;
        js.async = true;
        js.src = "//connect.facebook.net/en_US/all.js";
        ref.parentNode.insertBefore(js, ref);
    }(document));

        FB.init({
            appId: 1610232299056898,
            status: true,
            cookie: true,
            xfbml: true
        });
}

