export class CookieHandler{

    static GetAuthorizationCookie(){
        let isMatch = document.cookie.match("bearer\\s[A-z0-9\\.\\-\\_]+") !== null;
        if(isMatch){
            return document.cookie.match("bearer\\s[A-z0-9\\.\\-\\_]+")[0]
        }
        return "";
    }

    static GetAuthorizedHeader(){
        return new Headers({
                'Content-Type': 'application/json',
                'Authorization': this.GetAuthorizationCookie()
            });
    }
}