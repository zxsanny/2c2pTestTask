enum StatusEnum {
  A,
  R,
  D
}

export default interface TransactionView {
  id: string;
  payment: string;
  status: StatusEnum;
}
