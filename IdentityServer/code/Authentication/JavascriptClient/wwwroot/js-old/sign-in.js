
//手工方式实现Implicit Flow
var createState = function () {
    return "StateValueLonglonglong";
}
var createNonce = function () {
    return "NonceValueLonglonglong";
}

var signIn = function () {
    var redirectUri = "https://localhost:17005/Home/SingIn";
    var responseType = "id_token token";
    var scope = "openid ApiOne.read";
    var authUrl =
        "/connect/authorize/callback"
        + "?client_id=client_id_js"
        + "&redirect_uri=" + encodeURIComponent(redirectUri)
        + "&response_type=" + encodeURIComponent(responseType)
        + "&scope=" + encodeURIComponent(scope)
        + "&nonce=" + createNonce()
        + "&state=" + createState();
    var returnUrlEncode = encodeURIComponent(authUrl);

    console.log(authUrl)
    console.log(returnUrlEncode)

    window.location.href = "https://localhost:17001/Auth/Login?ReturnUrl=" + returnUrlEncode;
}