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

    static SetAuthorizationCookie(token){
        const today = new Date();
        const nextMonth = new Date(today.getFullYear(),
         today.getMonth() + 1, today.getDate()).toUTCString();
        document.cookie = "authorization=bearer " + token + "; expires=" + nextMonth  + "; path=/"
    }
}