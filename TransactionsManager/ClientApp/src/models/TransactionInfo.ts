enum StatusEnum {
  A,
  R,
  D
}

export default interface TransactionInfo {
  id: string;
  amount: number;
  currency: string;
  Date: Date;
  Status: StatusEnum;
}
