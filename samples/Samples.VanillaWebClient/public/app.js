Oidc.Log.logger = window.console;
Oidc.Log.level = Oidc.Log.DEBUG;

const root = window.location.origin;

const config = {
    authority: "https://localhost:5000/",

    client_id: "interactive-client",
    client_secret: "secret",

    redirect_uri: root + "/callback.html",
    post_logout_redirect_uri: root + "/index.html",

    // these two will be done dynamically from the buttons clicked, but are
    // needed if you want to use the silent_renew
    response_type: "code",
    scope: "api-1 openid profile",

    // silent renew will get a new access_token via an iframe
    // just prior to the old access_token expiring (60 seconds prior)
    silent_redirect_uri: root + "/silent.html",
    automaticSilentRenew: true,

    monitorAnonymousSession: true,

    // will revoke (reference) access tokens at logout time
    revokeAccessTokenOnSignout: true,
};

var mgr = new Oidc.UserManager(config);

function login() {
    mgr.signinRedirect();
}

function logout() {
    mgr.signoutRedirect();
}

function renew() {
    mgr.signinSilent()
        .then(() => {
            console.log("Token silently renewed");
            showTokens();
        }).catch(
            err => console.log("Error signing in", err)
        );
}

function revoke() {
    mgr.revokeAccessToken()
        .then(
            () => console.log("Access token revoked")
        ).catch(
            err => console.log("Error revoking access", err)
        );
}

const api_endpoint = "https://localhost:5200/Identity";

function call() {
    mgr.getUser().then(user => {
        if (user) {
            fetch(api_endpoint, {
                headers: {
                    Authorization: "Bearer " + user.access_token,
                },
            }).then(
                response => response.json()
            ).then(
                data => console.log(data)
            ).catch(
                err => console.log("Error revoking access", err)
            );
        } else {
            console.log("Not logged in");
        }
    });
}

document.querySelector("#login").addEventListener("click", login, false);
document.querySelector("#logout").addEventListener("click", logout, false);
document.querySelector("#renew").addEventListener("click", renew, false);
document.querySelector("#revoke").addEventListener("click", revoke, false);
document.querySelector("#call").addEventListener("click", call, false);

function showTokens() {
    mgr.getUser()
        .then(user => {
            if (user) {
                console.log("Logged in", user);
            }
            else {
                console.log("Not logged in");
            }
        });
}

showTokens();
