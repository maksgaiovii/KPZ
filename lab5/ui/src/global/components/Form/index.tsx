import { Controller, useForm } from "react-hook-form";
import { FormConfigProps } from "../../../types";
import { yupResolver } from "@hookform/resolvers/yup";
import { MyField } from "./Field";
import get from "lodash/get";
import { ErrorMessage, FieldGroup } from "./Field/field";
import { Button } from "./Field/button";
import { useCallback } from "react";

type MyFormProps = FormConfigProps<any> & {
  onSubmit: (data: any) => Promise<any>;
  closeModal: () => void;
  refetch?: () => void;
};

export const MyForm = ({
  fields,
  yupSchema,
  onSubmit: submit,
  beforeSendToBekend,
  handleBeckendResponse,
  closeModal,
  refetch,
}: MyFormProps) => {
  const { control, handleSubmit, formState, setError, clearErrors, reset } =
    useForm({
      resolver: yupResolver(yupSchema as any),
    });

  const onSubmit = useCallback(
    async (data) => {
      data = beforeSendToBekend ? beforeSendToBekend(data) : data;
      try {
        const res = await submit(data);
        handleBeckendResponse?.({
          clearErrors,
          closeModal,
          reset,
          response: res,
          setError,
          refetch,
        });

        if (!handleBeckendResponse) {
          closeModal();
        }
      } catch (error: any) {
        setError("root", {
          message: error?.message || "Something went wrong",
        });
      }
    },
    [
      beforeSendToBekend,
      submit,
      handleBeckendResponse,
      setError,
      clearErrors,
      closeModal,
      reset,
    ]
  );

  return (
    <form onSubmit={handleSubmit(onSubmit)}>
      <fieldset
        disabled={formState.isSubmitting}
        className={`${formState.isSubmitting ? "opacity-50" : ""}`}
      >
        <FieldGroup>
          {Object.keys(fields).map((fieldKey, index) => (
            <Controller
              key={index}
              control={control}
              name={fieldKey}
              render={({ field }) => (
                <MyField
                  field={field}
                  config={fields[fieldKey]}
                  error={
                    get(formState.errors, fieldKey) &&
                    (get(formState.errors, fieldKey)?.message as string)
                  }
                />
              )}
            />
          ))}
          <div className="text-end">
            <Button color="dark" type="submit">
              Submit
            </Button>
          </div>
          {formState.errors.root?.message && (
            <ErrorMessage>{formState.errors.root?.message}</ErrorMessage>
          )}
        </FieldGroup>
      </fieldset>
    </form>
  );
};
