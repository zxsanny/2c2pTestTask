import TransactionInfo from "./TransactionInfo";

interface ParserResult {
  success: boolean;
  transactions: TransactionInfo[];
  errors: string[];
}

interface InsertResult {
  inserted: number;
  updated: number;
}

export default interface UploadResult {
  ParserResult: ParserResult;
  InsertResult: InsertResult;
}
