import { NewWork } from './new-work';

export class GetWork extends NewWork {
  id: number;
  startTime: Date;
  endTime: Date;
  isConfirmed: boolean;
  isAuthor: boolean;
  priorityString: String;
}
