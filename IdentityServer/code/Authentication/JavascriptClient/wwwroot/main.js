var config = {
    authority: "https://localhost:7001",
    client_id: "client_id_js",
    redirect_uri: "https://localhost:7005/Home/SignIn",
    response_type: "id_token token",
    scope: "openid ApiOne.read",
};


var userManager = new Oidc.UserManager(config);

var signin = function () {
    userManager.signinRedirect();
}

userManager.getUser().then(user => {
    console.log("user:", user);
    if (user) {
        axios.defaults.headers.common["Authorization"] = "Bearer " + user.access_token;
    }
})

var callApi = function () {
    axios.get('https://localhost:7002/api/secret')
        .then(res => {
            console.log(res);
        });
}