import {Injectable} from '@angular/core';
import {AuthService} from "./auth.service";
import {ConfigService} from "./config.service";
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {Project} from "../project";
import {CardAssignToProjectRequest} from "../CardAssignToProjectRequest";

@Injectable({
  providedIn: 'root'
})
export class ProjectApiService {

  constructor(
    private authService:AuthService,
    private configService:ConfigService,
    private httpClient: HttpClient
  ) { }

  public getProjects():Observable<Project[]>
  {
    let username = null;
    if(this.authService.loggedIn){
      username = this.authService.getUserName();
    }
    return this.httpClient.get<Project[]>(
      `${this.configService.master_apiURL}/projects/${username}`
    );

  }

  AddProject(Id: number, Title: string):Observable<Project> {
    let username = null;
    if(this.authService.loggedIn){
      username = this.authService.getUserName();
    }
    let project=new Project();
    project.title=Title;
    project.id=Id;
    return this.httpClient.post<Project>(`${this.configService.master_apiURL}/projects/${username}`,project)
  }

  GetCardsForProject(id: any) {
    return this.httpClient.get<Project>(`${this.configService.master_apiURL}/projects/GetForProjectId/${id}`);
       }


  AssignCardToProject(cardId: any, selectedProject: any) {
    const request = new CardAssignToProjectRequest();
    request.cardId=cardId;
    request.projectId=selectedProject;
   return this.httpClient.post(`${this.configService.master_apiURL}/projects/AddCardToProject/${selectedProject}`,request)
  }
}

