export class Connection {
  public description: string;
  public words: ConnectionsWord[];
  constructor(description: string, words: ConnectionsWord[]) {
    this.description = description;
    this.words = words;
  }
}
export class ConnectionsWord {
  public word: string;
  public state: ConnectionState;
  constructor(word: string) {
    this.word = word;
    this.state = ConnectionState.Unknown;
  }
}
export enum ConnectionState {
  Unknown = 0,
  Correct = 1,
  Wrong = 2,
  Clicked = 3,
}
