import React, { useEffect } from "react";
import { gsap } from "gsap";
import "../congratulations.css";
import { StarFilled } from "@ant-design/icons";
import { Typography } from "antd";
const { Title } = Typography;

interface ICongratulationsProps {
  title: string;
  starsCount: number;
}

const Congratulations: React.FC<ICongratulationsProps> = ({
  title,
  starsCount,
}) => {
  useEffect(() => {
    const initialBodyOverflow = document.body.style.overflow;

    const animateBlobs = () => {
      const blobs = document.querySelectorAll(".blob");

      blobs.forEach((blob: any) => {
        gsap.to(blob, {
          x: gsap.utils.random(-350, 750),
          y: gsap.utils.random(-120, 170),
          duration: gsap.utils.random(1, 5),
          opacity: 0,
          rotation: gsap.utils.random(5, 100),
          scale: gsap.utils.random(0.8, 5.5),
          onStart: () => {
            document.body.style.overflow = "hidden";
          },
          onComplete: () => {
            document.body.style.overflow = initialBodyOverflow;
            blob.style.display = "none";
          },
        });
      });
    };

    animateBlobs();
    return () => {
      document.body.style.overflow = initialBodyOverflow;
    };
  }, []);

  return (
    <div className="congrats">
      <Title level={3}>{title}</Title>
      {Array.from({ length: starsCount }).map((item, index) => (
        <StarFilled key={index} className="blob" />
      ))}
    </div>
  );
};

export default Congratulations;
