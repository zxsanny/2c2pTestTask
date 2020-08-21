import { Component, Inject, Output, EventEmitter, OnInit } from '@angular/core';
import { HttpClient, HttpEventType } from '@angular/common/http';
import { NotifierService } from 'angular-notifier';

@Component({
  selector: 'file-upload',
  templateUrl: './file-upload.component.html',
  styleUrls: ['./file-upload.component.css']
})

export class FileUploadComponent implements OnInit {
  public progress: number;
  public message: string;
  @Output() public onUploadFinished = new EventEmitter();

  constructor(private notifierService: NotifierService, private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  ngOnInit() {}

  public uploadFile = (files: FileList) => {
    if (files.length === 0) {
      return;
    }
    const fileToUpload = files.item(0);
    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);

    this.http.post(this.baseUrl + 'transaction/upload', formData, { reportProgress: true, observe: 'events' })
      .subscribe(event => {
        if (event.type === HttpEventType.UploadProgress) {
          this.progress = Math.round(event.loaded * 100 / event.total);
        } else if (event.type === HttpEventType.Response) {
          this.message = 'Upload success';
          this.onUploadFinished.emit(event.body);
        }
      }, (errorMessage) => this.notifierService.notify('error', errorMessage.error ));
  };
}
