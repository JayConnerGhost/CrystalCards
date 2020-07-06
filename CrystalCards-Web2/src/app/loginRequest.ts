export class LoginRequest {
    constructor(Username: string, Password: string) {
        this.Username = Username;
        this.Password = Password;
    }

    Username: string;
    Password: string;
}
