import { CookieHandler } from "../cookies/CookieHandler";

export class AuthenticationHandler {
    static IsLogged() {
        return this.#areClaimsCorrect();
    }

    static #areClaimsCorrect() {
        const result = this.#getObject()
        return "http://schemas.microsoft.com/ws/2008/06/identity/claims/role" in result 
        && "http://schemas.xmlsoap.org/ws/2009/09/identity/claims/actor" in result;
    }

    static #getObject() {
        const token = CookieHandler.GetAuthorizationCookie();
        try {
            return JSON.parse(atob(token.split('.')[1]));
        } catch (e) {
            return {};
        }
    }
}