export class Connection {
  public description: string;
  public words: string[];
  public connectionState: ConnectionState = ConnectionState.Unknown;
  constructor(description: string, words: string[]) {
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
}
