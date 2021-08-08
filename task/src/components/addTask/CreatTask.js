import React from 'react';
import './addTask.css'
function CreateTask(props){
    return (        
      <div>
        <input type="text"  value={props.value} onChange={props.onChange} />
        <button onClick={props.addTask}>Add Task</button>
      </div>
      )
}

export default CreateTask;
