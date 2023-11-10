import { Component, EventEmitter, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-chat-input',
  templateUrl: './chat-input.component.html',
  styleUrls: ['./chat-input.component.css']
})
export class ChatInputComponent implements OnInit {

  content:string='';
  @Output() contentEmitter=new EventEmitter();
  constructor() { }

  ngOnInit(): void {
  }

  SendeMessage(){
    //debugger;
    if(this.content.trim()!==""){
      this.contentEmitter.emit(this.content);
    }

    this.content='';

  }


}
