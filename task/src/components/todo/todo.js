import React from "react";
import "./todo.css";

function Todo(props) {
  const todo = props.todo;
  const completeTask = props.completeTask;

  return (
    <div className="todo">
    
      <div className="content">
        <div className="task">
          <li>
            {todo.split(",")[0]}
          
          </li>
          <button className="btn btn-info" onClick={completeTask}>
              Done
            </button>
        </div>
      </div>
    </div>
  );
}

export default Todo;
