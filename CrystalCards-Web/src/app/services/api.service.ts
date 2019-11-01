import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {Card} from ".././card";
import {Observable} from "rxjs";
import {ConfigService} from "./config.service";
import {AuthService} from "./auth.service";
import {User} from "../User";

@Injectable({
  providedIn: "root"
})
export class ApiService {

  constructor(
    private httpClient: HttpClient,
    private configService: ConfigService,
    private authService: AuthService
  ) {}

  DeleteCard(event: any) {
    console.log("deleting",event)
    return this.httpClient.delete(`${this.configService.master_apiURL}/cards/${event}`);
  }


  NewBasic(Id: number, Title: any, Description: any): Observable<Card> {
    let card = new Card();
    card.description = Description;
    card.title = Title;
    card.id = Id;
    let username = null;
    if(this.authService.loggedIn){
      username = this.authService.getUserName();
    }
    return this.httpClient.post<Card>(
      `${this.configService.master_apiURL}/cards/${username}`,
      card
    );
  }
  public getCards(): Observable<Card[]> {
    let username = null;
    if(this.authService.loggedIn){
      username = this.authService.getUserName();
    }
    return this.httpClient.get<Card[]>(
      `${this.configService.master_apiURL}/cards/GetForUserName/${username}`
    );
  }

  public update(
    title,
    description,
    id,
    points,
    actionPoints,
    links
  ): Observable<Card> {
    console.log(title, description, id, points);
    let card = new Card();
    card.description = description;
    card.title = title;
    card.id = id;
    card.npPoints = points;
    card.actionPoints = actionPoints;
    card.links = links;

    return this.httpClient.put<Card>(
      `${this.configService.master_apiURL}/cards/${id}`,
      card
    );
  }

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
    //TODO.
  }
}
