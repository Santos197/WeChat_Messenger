import { Component, EventEmitter, OnDestroy, OnInit, Output } from '@angular/core';
import { ChatService } from '../services/chat.service';
import { NgbModalBackdrop } from '@ng-bootstrap/ng-bootstrap/modal/modal-backdrop';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { PrivateChatComponent } from '../private-chat/private-chat.component';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit,OnDestroy {

  @Output() closechatEmitter=new EventEmitter();

  
  constructor(public chatService:ChatService,private modalService:NgbModal) { }

  ngOnDestroy(): void {
  this.chatService.stopChatConnection();
  }

  ngOnInit(): void {
    this.chatService.createChatConnection();
  }

  backToHome()
  {
    this.closechatEmitter.emit();
  }

  SendMessage(content:string)
  {
    this.chatService.sendMessage(content);
  }

  openPrivateChat(to:string)
  {
    //debugger;
   const modalRef=this.modalService.open(PrivateChatComponent);
   modalRef.componentInstance.toUser=to; 
  }
  
  
}
