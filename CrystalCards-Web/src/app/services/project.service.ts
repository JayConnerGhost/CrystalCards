import { BehaviorSubject } from 'rxjs';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {
  loadGeneralEvent: BehaviorSubject<void>;
  LoadProjectEvent: BehaviorSubject<number>=new BehaviorSubject<number>(-1);
  constructor() {

    this.loadGeneralEvent=new BehaviorSubject<void>(null);
  }

  LoadProjectData(projectId){
    localStorage.setItem('projectContext', projectId);
    this.LoadProjectEvent.next(projectId);
  }
  LoadGeneralData(){
    localStorage.setItem('projectContext', null);
    this.loadGeneralEvent.next();
  }
}
