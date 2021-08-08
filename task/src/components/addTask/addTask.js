import { Component } from "react";
import "./addTask.css";
import axios from "axios";

class AddTask extends Component {
    constructor(props){super(props)
    this.state={task:""}
}


onChangeHndler=e=>{this.setState({[e.target.name]:e.target.value})}
onSubmitHndler=e=>{e.preventDefault();
    const task =  axios.post(
        `https://api.trello.com/1/cards?key=9a9642ad897bdc7f5aed8a16c4fb0bfc&token=1b9cc73d2a0bbaf715d7087b503798232b6f7baaa8aefb926aa26727cf1ff3ab&idList=610ae016034205220a9fc046&name=${this.state.task}`);
      //  window.location.reload();
        return task;
    };
  render() {
      const {task}=this.state;
    return (

        
    <div className="containter">
    <form onSubmit={this.onSubmitHndler} className="task-form">
        <input name="task" type="text" className="  form-control" value={task} onChange={this.onChangeHndler}/>
        <button type="submit"  className=" btn btn-info" > Add Task</button>
    </form>
     </div>
    )  }


};
export default AddTask;
