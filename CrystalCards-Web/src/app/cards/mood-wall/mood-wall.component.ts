import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { HttpEventType, HttpClient} from '@angular/common/http';

@Component({
  selector: 'app-mood-wall',
  templateUrl: './mood-wall.component.html',
  styleUrls: ['./mood-wall.component.css']
})
export class MoodWallComponent implements OnInit {
//Whole class target for refactor KM 07102019
//reference: https://code-maze.com/upload-files-dot-net-core-angular/

public progress: number;
public message: string;
@Output() public UploadFinished= new EventEmitter();

  constructor(private http: HttpClient) { }

  ngOnInit() {
  }

  public uploadFile =(files)=>{
    if (files.length === 0) {
      return;
    }
    let fileToUpload =<File>files[0];
    const formData =new FormData();
    formData.append('file',fileToUpload, fileToUpload.name);

    //horrible
    this.http.post('http://localhost:50872/api/MoodWall', formData, {reportProgress: true, observe: 'events'})
    .subscribe(event => {
      if (event.type === HttpEventType.UploadProgress)
        this.progress = Math.round(100 * event.loaded / event.total);
      else if (event.type === HttpEventType.Response) {
        this.message = 'Upload success.';
        this.UploadFinished.emit(event.body);
      }
    });

  }

  }
