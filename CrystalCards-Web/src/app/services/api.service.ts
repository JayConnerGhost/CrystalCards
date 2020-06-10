import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {ConfigService} from "./config.service";
import {AuthService} from "./auth.service";
import {User} from "../User";
import {CustomRoleAssignmentRequest} from "../Role";

@Injectable({
  providedIn: "root"
})
export class ApiService {

  constructor(
    private httpClient: HttpClient,
    private configService: ConfigService,
    private authService: AuthService
  ) {}

  GetImageURLs(userId, cardId): any {
    console.log(cardId);

    return this.httpClient.get<string[]>(
      `${this.configService.master_apiURL}/moodwall/${userId}/${cardId}`
    );
  }

  getUsers() {
     return this.httpClient.get<User[]>(
       `${this.configService.master_apiURL}/users`
     );
  }

  deleteUser(username: string) {
    return this.httpClient.delete(`${this.configService.master_apiURL}/users/${username}`);
  }

  changeAdminOnUser(username: string, checked: boolean) {
    //TODO. implement client calls to API to add and remove admin role
    if(checked){
      //make a role object
      let request=new CustomRoleAssignmentRequest();
      request.roleName="Administrator";
      request.userName=username;
     return this.httpClient.post(`${this.configService.master_apiURL}/roles`,
        request
        );

    }
    else {
      let request=new CustomRoleAssignmentRequest();
      request.roleName="Administrator";
      request.userName=username;
      //call Remove as post on roles service
      return this.httpClient.post(`${this.configService.master_apiURL}/roles/remove`,
        request
      );
    }
  }
}
