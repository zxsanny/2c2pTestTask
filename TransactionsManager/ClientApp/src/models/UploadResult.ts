import TransactionInfo from "./TransactionView";

interface ParserResult {
  success: boolean;
  transactions: TransactionInfo[];
  errors: string[];
}

interface InsertResult {
  success: boolean;
  error: string;
  inserted: number;
  updated: number;
}

export default interface UploadResult {
  parserResult: ParserResult;
  insertResult: InsertResult;
}
