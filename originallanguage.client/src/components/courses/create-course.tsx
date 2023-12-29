import { useState } from "react";
import axios from "axios";
import { ICourse } from "../../models/ICourse";

interface ICreateCourseProps {
  onCreate: (newCourse: ICourse) => void;
}

export function CreateCourse({ onCreate }: ICreateCourseProps) {
  const [title, setTitle] = useState("");

  const handleTitleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setTitle(event.target.value);
  };
  const handleSubmit = async (event: React.FormEvent) => {
    event.preventDefault();

    const response = await axios.post<ICourse>(
      import.meta.env.VITE_API_URL + "courses",
      {
        authorId: "a765ff05-813b-4a63-adf6-c3697ed77037", // Todo: actual author
        title,
      }
    );

    console.log("Course created: ", response);
    setTitle("");

    onCreate(response.data);
  };

  return (
    <form onSubmit={handleSubmit}>
      <input
        type="text"
        value={title}
        onChange={handleTitleChange}
        placeholder="Course title"
        required
      />
      <button>Create</button>
    </form>
  );
}
