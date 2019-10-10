import { Component, OnInit, Output, EventEmitter, Input } from "@angular/core";
import { HttpEventType, HttpClient } from "@angular/common/http";
import { ApiService } from "src/app/api.service";
import { ConfigService } from 'src/app/config.service';
import { FilePreviewOverlayRef } from 'src/app/file-preview-overlay-ref';
import { FilePreviewOverlayService, Image2 } from 'src/app/file-preview-overlay.service';



@Component({
  selector: "app-mood-wall",
  templateUrl: "./mood-wall.component.html",
  styleUrls: ["./mood-wall.component.css"]
})
export class MoodWallComponent implements OnInit {
  //Whole class target for refactor KM 07102019
  //reference: https://code-maze.com/upload-files-dot-net-core-angular/

  public progress: number;
  public message: string;
  public images: string[];
  @Input() cardId: string;
  @Output() public UploadFinished = new EventEmitter();

  constructor(
    private http: HttpClient,
    private apiService: ApiService,
    private configService: ConfigService,
    private previewDialog: FilePreviewOverlayService
    ) {}

  ngOnInit() {
    this.getImageURLs();

  }

  getImageURLs() {
    console.log(this.cardId);
    this.apiService.GetImageURLs(this.cardId).subscribe(res => {
      this.images = res.map(x=> `${this.configService.Images}/${this.cardId}/${x}`);

    });
  }

  ImageClicked(url){
    console.log(url);
    var selectedImage=new Image2();
    selectedImage.url=url;
    selectedImage.name="test";
    let dialogRef: FilePreviewOverlayRef = this.previewDialog.open({
      image: selectedImage
    });

  }

  public uploadFile = files => {
    if (files.length === 0) {
      return;
    }
    let fileToUpload = <File>files[0];
    const formData = new FormData();
    formData.append("file", fileToUpload, fileToUpload.name);
    formData.append("cardId",this.cardId)
    //horrible
    this.http
      .post(`${this.configService.master_apiURL}/MoodWall`, formData, {
        reportProgress: true,
        observe: "events"
      })
      .subscribe(event => {
        if (event.type === HttpEventType.UploadProgress)
          this.progress = Math.round((100 * event.loaded) / event.total);
        else if (event.type === HttpEventType.Response) {
          this.message = "Upload success.";
          this.UploadFinished.emit(event.body);
         this.getImageURLs();
        }
      });
  };
}
