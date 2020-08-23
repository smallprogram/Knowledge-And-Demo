var config = {
    userStore: new Oidc.WebStorageStateStore({ store: window.localStorage }), // 告诉oidc从localStorage读取已认证的用户数据
    authority: "https://localhost:17001",
    client_id: "client_id_js",
    redirect_uri: "https://localhost:17005/Home/SignIn",
    post_logout_redirect_uri: "https://localhost:17005/Home/Index",
    response_type: "code",
    scope: "openid ApiOne.read role.scope",
};


var userManager = new Oidc.UserManager(config);

var signIn = function () {
    userManager.signinRedirect();
}

var signOut = function () {
    userManager.signoutRedirect();
}




userManager.getUser().then(user => {
    console.log("user:", user);
    if (user) {
        axios.defaults.headers.common["Authorization"] = "Bearer " + user.access_token;
    }
})

var callApi = function () {
    axios.get('https://localhost:17002/api/secret')
        .then(res => {
            console.log(res);
        });
}

var refreshing = false;
axios.interceptors.response.use(
    function (response) { return response },
    function (error) {
        console.log("axios Error:", error.response);
        var axiosConfig = error.response.config;

        // 如果状态是401，refresh token
        if (error.response.status === 401) {
            console.log("axios Error 401:", error.response);

            // 如果已经刷新了token就不要再刷新了
            if (!refreshing) {
                console.log("开始更新Token");
                refreshing = true;

                // do refresh token
                return userManager.signinSilent().then(user => {
                    console.log("new user:",user);
                    // update httpClient
                    axios.defaults.headers.common["Authorization"] = "Bearer " + user.access_token;
                    // update http request
                    axiosConfig.headers["Authorization"] = "Bearer " + user.access_token;

                    // 重新发送请求
                    return axios(axiosConfig);
                });
            }

        }

        return Promise.reject(error);
    },

)

