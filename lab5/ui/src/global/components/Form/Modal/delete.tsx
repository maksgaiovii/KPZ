import { useCallback, useState } from "react";
import {
  Dialog,
  DialogActions,
  DialogBody,
  DialogDescription,
  DialogTitle,
} from ".";
import { Button } from "../Field/button";

type DeleteModalProps = {
  isModalOpen: boolean;
  closeModal: () => void;
  onSubmit: () => Promise<void>;
};

export const DeleteModal = ({
  isModalOpen,
  closeModal,
  onSubmit,
}: DeleteModalProps) => {
  const [isLoading, setIsLoading] = useState(false);

  const deleteAction = useCallback(async () => {
    setIsLoading(true);
    try {
      await onSubmit();
    } finally {
      setIsLoading(false);
    }
    closeModal();
  }, [closeModal, onSubmit]);

  return (
    <Dialog
      open={isModalOpen}
      onClose={isLoading ? () => {} : closeModal}
      size="sm"
    >
      <DialogTitle>{"Delete"}</DialogTitle>
      <DialogBody>
        <DialogDescription>
          Are you sure you want to delete this item?
        </DialogDescription>
        <DialogActions>
          <Button onClick={closeModal} disabled={isLoading}>
            Cancel
          </Button>
          <Button onClick={deleteAction} disabled={isLoading} color="red">
            Delete
          </Button>
        </DialogActions>
      </DialogBody>
    </Dialog>
  );
};