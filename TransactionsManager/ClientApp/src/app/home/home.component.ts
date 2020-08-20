import { Component, Inject } from '@angular/core';
import { NotifierService } from "angular-notifier";
import UploadResult from '../../models/UploadResult';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {

  constructor(private notifierService: NotifierService, private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  reloadTransactions = () => {
    this.http.get(this.baseUrl + 'transaction').subscribe(data => {
      //view data
    });
  };

  onUpload = (event: UploadResult) => {
    if (!event.parserResult.success) {
      this.notifierService.show({
        message: event.parserResult.errors.join(', '),
        type: "error"
      });
    }
    else {
      this.notifierService.notify('info', `Inserted: ${event.insertResult.inserted}, updated: ${event.insertResult.updated}`);
      this.reloadTransactions();
    }
  };
}
