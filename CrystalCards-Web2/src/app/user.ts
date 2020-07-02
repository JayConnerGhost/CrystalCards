export class User {
  constructor(FirstName: string, SecondName: string, Username: string, Password: string) {
    this.firstName = FirstName;
    this.secondName = SecondName;
    this.userName = Username;
    this.password = Password;
  }

  iD: number;
  firstName: string;
  secondName: string;
  userName: string;
  password: string;
}
