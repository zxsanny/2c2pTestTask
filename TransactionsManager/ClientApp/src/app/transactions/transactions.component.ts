import { Component, Inject } from '@angular/core';
import { NotifierService } from "angular-notifier";
import UploadResult from '../../models/UploadResult';
import { HttpClient } from '@angular/common/http';
import TransactionView from '../../models/TransactionView';

@Component({
  selector: 'app-home',
  templateUrl: './transactions.component.html',
})
export class TransactionsComponent {

  constructor(private notifierService: NotifierService, private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    this.reloadTransactions();
  }
  public transactions: TransactionView[];

  reloadTransactions = () => {
    this.http.get<TransactionView[]>(this.baseUrl + 'transaction').subscribe(data => {
      this.transactions = data;
    },
      errorResponse => this.notifierService.notify('error', errorResponse.error));
  };

  onUpload = (event: UploadResult) => {
    this.notifierService.notify('info', `Parsed and inserted: ${event.insertResult.inserted}, updated: ${event.insertResult.updated}`);
    this.reloadTransactions();
  };
}
